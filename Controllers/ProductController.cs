using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace AuFood.Controllers
{
    public class Product_list : Product
    {
        public double Price { get; set; }
    }

    public class Product_category
    {
        public string Category_name { get; set; }

        public int Category_id { get; set; }

        public List<Product_list> List_product { get; set; }
    }

    public class ListInt
    {
        public string list_id { get; set; }
    }

    public class IParams
    {
        public string q { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly _DbContext _context;

        public ProductController(_DbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("list_product_cart/{store_id}")]
        public async Task<IEnumerable<Product>> GetListProduct([FromQuery] ListInt pParams, int store_id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);

            var listIds = JsonConvert.DeserializeObject<List<int>>(pParams.list_id);

            var ListProduct = await _context.Product
                .Where(w => listIds.Contains(w.Id) && ListProductOnStore.Contains(w.Id))
                .Select(p => new Product_list
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Product_price
                        .Where(pp => pp.Day_week == DateTime.Now.DayOfWeek)
                        .Select(pp => (double?)pp.Price)
                        .FirstOrDefault() ?? 0.00,
                    Product_category = p.Product_category,
                    Image = p.Image,
                    Description = p.Description
                })
                .ToListAsync();

            return ListProduct;
        }

        [AllowAnonymous]
        [HttpGet("info_product/{product_id}")]
        public async Task<ActionResult<Product>> GetInfoProduct(int product_id)
        {
            var Product = await _context.Product
                .Where(w => w.Id == product_id)
                .Select(p => new Product_list
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Product_price
                        .Where(pp => pp.Day_week == DateTime.Now.DayOfWeek)
                        .Select(pp => (double?)pp.Price)
                        .FirstOrDefault() ?? 0.00,
                    Product_category = p.Product_category,
                    Image = p.Image,
                    Description = p.Description
                })
                .FirstOrDefaultAsync();

            if (Product == null)
                return NotFound();

            return Product;
        }

        [AllowAnonymous]
        [HttpGet("list_all/{id}")]
        public async Task<IEnumerable<Product_list>> GetListProduct(int id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, id);

            var ListProduct = await _context.Product
                .Include(w => w.Product_category)
                .Include(w => w.Product_price)
                .Where(w => ListProductOnStore.Contains(w.Id))
                .Select(p => new Product_list
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Product_price
                        .Where(pp => pp.Day_week == DateTime.Now.DayOfWeek)
                        .Select(pp => (double?)pp.Price)
                        .FirstOrDefault() ?? 0.00, 
                    Product_category = p.Product_category,
                    Image = p.Image,
                    Description = p.Description
                })
                .ToListAsync();

            return ListProduct;
        }
        
        [AllowAnonymous]
        [HttpGet("search_product_store/{store_id}")]
        public async Task<IEnumerable<Product_list>> SearchProduct(int store_id, [FromQuery] IParams pParams)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);
            pParams.q = pParams.q.ToLower();

            var ListProduct = _context.Product
                .Include(w => w.Product_category)
                .Include(w => w.Product_price)
                .Where(w => ListProductOnStore.Contains(w.Id))
                .Select(p => new Product_list
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Product_price
                        .Where(pp => pp.Day_week == DateTime.Now.DayOfWeek)
                        .Select(pp => (double?)pp.Price)
                        .FirstOrDefault() ?? 0.00,
                    Product_category = p.Product_category,
                    Image = p.Image,
                    Description = p.Description
                })
                .AsQueryable();

            var ListFilterNameProduct = ListProduct.Where(w => w.Name.ToLower().Contains(pParams.q)).AsQueryable();

            //If you can't find a product by name, search for products by category name
            if (!ListFilterNameProduct.Any())
            {
                ListProduct = ListProduct.Where(w => w.Product_category.Name.ToLower().Contains(pParams.q)).AsQueryable();
            }
            else
            {
                ListProduct = ListFilterNameProduct;
            }

            return await ListProduct.ToListAsync();
        }
        
        [HttpPost]
        public async Task<Product> Post(Product product)
        {
            product.Product_store = product
                .List_store_id
                .Select(w => new Product_store
                {
                    Product = product,
                    Store_id = w
                })
                .ToList();

            product.Image = "";
            
            _context.Product.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product newProduct)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var product = await _context.Product
                .Include(w => w.Product_store)
                .Where(w => w.Id == newProduct.Id)
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            if (!product.Product_store.Any(w => vStoresID.Contains(w.Store_id)))
                return Unauthorized();

            if(newProduct != null && newProduct.Product_price!.Count > 0)
            {
                newProduct.Product_price = newProduct.Product_price.Select(w => new Product_price
                {
                    Day_week = w.Day_week,
                    Price = (double)w.Price,
                    Product_id = product.Id
                }).ToList();
            }

            newProduct.SerializeProps(ref product);

            product.Image = "";

            foreach (var store_id in newProduct.List_store_id)
            {
                if (!product.Product_store.Any(cc => cc.Store_id == store_id))
                {
                    var new_product_store = new Product_store
                    {
                        Product_id = product.Id,
                        Store_id = store_id
                    };

                    product.Product_store.Add(new_product_store);
                }
            }

            await _context.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// Method for create new Product
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{product_id}")]
        public async Task<ActionResult<Product>> Delete(int product_id)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var Product = await _context.Product.FindAsync(product_id);

            if(Product == null)
                return NotFound();

            if (!Product.Product_store.Any(w => vStoresID.Contains(w.Store_id)))
                return Unauthorized();

            _context.Product.Remove(Product);

            await _context.SaveChangesAsync();

            return Product;
        }

        [HttpGet("{product_id}")]
        public async Task<ActionResult<Product>> GetProduct(int product_id)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var Product = await _context.Product
                .Include(w => w.Product_store)
                .Include(w => w.Product_price)
                .Where(w => w.Id == product_id)
                .FirstOrDefaultAsync();

            if(Product == null)
                return NotFound();

            if(!Product.Product_store.Any(w => vStoresID.Contains(w.Store_id)))
                return Unauthorized();

            Product.List_store_id = Product.Product_store.Select(w => w.Store_id).ToList();

            return Product;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<Product>> GetListProduct()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var ListProduct = await _context.Product
                .Include(w => w.Product_store)
                .Where(w => w.Product_store.Any(p => vStoresID.Contains(p.Store_id)))
                .ToListAsync();

            return ListProduct;
        }
    }
}
