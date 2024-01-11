using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    public class Product_list
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public double Price { get; set; }

        public string Time_delivery { get; set; }

        public Models.Product_category? product_category { get; set; }

        public string Image { get; set; }
    }

    public class Product_category
    {
        public string Category_name { get; set; }

        public int Category_id { get; set; }

        public List<Product_list> List_product { get; set; }
    }

    public class ListInt
    {
        public List<int> list_id { get; set; }
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

        /// <summary>
        /// Method for get list all products
        /// </summary>
        /// <param name="id">ID for Store</param>
        /// <returns></returns>
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
                    product_category = p.Product_category,
                    Image = p.Image
                })
                .ToListAsync();

            return ListProduct;
        }

        /// <summary>
        /// Method for search for product's
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <param name="pParams">Params for search</param>
        /// <returns></returns>
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
                    product_category = p.Product_category,
                    Image = p.Image
                })
                .AsQueryable();

            var ListFilterNameProduct = ListProduct.Where(w => w.Name.ToLower().Contains(pParams.q)).AsQueryable();

            //If you can't find a product by name, search for products by category name
            if (!ListFilterNameProduct.Any())
            {
                ListProduct = ListProduct.Where(w => w.product_category.Name.ToLower().Contains(pParams.q)).AsQueryable();
            }
            else
            {
                ListProduct = ListFilterNameProduct;
            }

            return await ListProduct.ToListAsync();
        }

        /// <summary>
        /// Method for get product's on order store
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_product_order/{store_id}")]
        public async Task<IEnumerable<Product>> GetListProduct([FromQuery] ListInt pParams, int store_id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);

            var ListProduct = await _context.Product
                .Where(w => pParams.list_id.Contains(w.Id) && ListProductOnStore.Contains(w.Id))
                .ToListAsync();

            return ListProduct;
        }
        
        //[AllowAnonymous]
        //[HttpGet("list_all_on_category/{id}")]
        //public async Task<List<Product_category>> GetListProductOnCategory(int id)
        //{
        //    var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, id);

        //    var ListProduct = await _context.Product_category
        //        .Include(w => w.Product)
        //            .ThenInclude(w => w.Product_price)
        //        .Where(w => w.Product.Any(p => ListProductOnStore.Contains(p.Id)))
        //        .Select(w => new Product_category
        //        {
        //            Category_id = w.Id,
        //            Category_name = w.Name,
        //            List_product = w.Product
        //            .Where(w => w.Product_price.Any(pp => pp.Day_week == DateTime.Now.DayOfWeek))
        //            .Select(p => new Product_list
        //            {
        //                Id = p.Id,
        //                Name = p.Name,
        //                Price = p.Product_price.Where(w => w.Day_week == DateTime.Now.DayOfWeek).First().Price,
        //                Image = p.Image
        //            })
        //            .ToList()
        //        })
        //        .ToListAsync();

        //    return ListProduct;
        //}

        /// <summary>
        /// Method for create new Product
        /// </summary>
        /// <returns></returns>
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

        [HttpPut("{product_id}")]
        public async Task<ActionResult<Product>> Update(Product product, int product_id)
        {
            var product_update = await _context.Product
                .Include(w => w.Product_store)
                .Where(w => w.Id == product_id)
                .FirstOrDefaultAsync();

            if (product_update == null)
            {
                return NotFound();
            }

            if(product != null && product.Product_price!.Count > 0)
            {
                product.Product_price = product.Product_price.Select(w => new Product_price
                {
                    Day_week = w.Day_week,
                    Price = (double)w.Price,
                    Product_id = product_update.Id
                }).ToList();
            }

            product.SerializeProps(ref product_update);

            product_update.Image = "";

            foreach (var store_id in product.List_store_id)
            {
                if (!product_update.Product_store.Any(cc => cc.Store_id == store_id))
                {
                    var new_product_store = new Product_store
                    {
                        Product_id = product_update.Id,
                        Store_id = store_id
                    };

                    product_update.Product_store.Add(new_product_store);
                }
            }

            await _context.SaveChangesAsync();

            return product_update;
        }

        /// <summary>
        /// Method for create new Product
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{product_id}")]
        public async Task<ActionResult<Product>> Delete(int product_id)
        {
            var Product = await _context.Product.FindAsync(product_id);

            if(Product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(Product);

            await _context.SaveChangesAsync();

            return Product;
        }

        /// <summary>
        /// Method for create new Product
        /// </summary>
        /// <returns></returns>
        [HttpGet("{product_id}")]
        public async Task<ActionResult<Product>> GetProduct(int product_id)
        {
            var Product = await _context.Product
                .Include(w => w.Product_store)
                .Include(w => w.Product_price)
                .Where(w => w.Id == product_id)
                .FirstOrDefaultAsync();

            if(Product == null)
            {
                return NotFound();
            }

            Product.List_store_id = Product.Product_store.Select(w => w.Store_id).ToList();

            return Product;
        }

        /// <summary>
        /// Method for get list all products
        /// </summary>
        /// <param name="id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_all")]
        public async Task<IEnumerable<Product>> GetListProduct()
        {
            var ListProduct = await _context.Product
                .ToListAsync();

            return ListProduct;
        }
    }
}
