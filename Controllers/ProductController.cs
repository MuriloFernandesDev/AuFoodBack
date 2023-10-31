using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    public class ProductList
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public double Price { get; set; }

        public string TimeDelivery { get; set; }

        public ProductCategory? productCategory { get; set; }

        public string Image { get; set; }
    }

    public class ProductOnCategory
    {
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public List<ProductList> ListProduct { get; set; }
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
        [HttpGet("list_all/{id}")]
        public async Task<IEnumerable<ProductList>> GetListProduct(int id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, id);

            var ListProduct = await _context.Product
                .Include(w => w.ProductCategory)
                .Include(w => w.ProductsPrice)
                .Where(w => ListProductOnStore.Contains(w.Id))
                .Select(p => new ProductList
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.ProductsPrice
                        .Where(pp => pp.DayWeek == DateTime.Now.DayOfWeek)
                        .Select(pp => (double?)pp.Price)
                        .FirstOrDefault() ?? 0.00, // Use DefaultIfEmpty to provide a default value if no price is found
                    TimeDelivery = p.TimeDelivery.ToString(),
                    productCategory = p.ProductCategory,
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
        [HttpGet("search_product_store/{store_id}")]
        public async Task<IEnumerable<ProductList>> SearchProduct(int store_id, [FromQuery] IParams pParams)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);
            pParams.q = pParams.q.ToLower();

            var ListProduct = _context.Product
                .Include(w => w.ProductCategory)
                .Include(w => w.ProductsPrice)
                .Where(w => ListProductOnStore.Contains(w.Id))
                .Select(p => new ProductList
                {
                    Id = p.Id,
                    Name = p.Name,
                    productCategory = p.ProductCategory,
                    Image = p.Image
                })
                .AsQueryable();

            var ListFilterNameProduct = ListProduct.Where(w => w.Name.ToLower().Contains(pParams.q)).AsQueryable();

            //If you can't find a product by name, search for products by category name
            if (!ListFilterNameProduct.Any())
            {
                ListProduct = ListProduct.Where(w => w.productCategory.Name.ToLower().Contains(pParams.q)).AsQueryable();
            }
            else
            {
                ListProduct = ListFilterNameProduct;
            }

            return await ListProduct.ToListAsync();
        }

        /// <summary>
        /// Method for get product's on cart store
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_product_cart/{store_id}")]
        public async Task<IEnumerable<Product>> GetListProduct([FromQuery] ListInt pParams, int store_id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);

            var ListProduct = await _context.Product
                .Where(w => pParams.list_id.Contains(w.Id) && ListProductOnStore.Contains(w.Id))
                .ToListAsync();

            return ListProduct;
        }

        /// <summary>
        /// Method for Retrieve all products by category.
        /// Fetch the price for each product determined by the current day of the week. 
        /// If there is no price registered for the current day of the week, do not include the product to avoid displaying a zero price.
        /// </summary>
        /// <param name="id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_all_on_category/{id}")]
        public async Task<List<ProductOnCategory>> GetListProductOnCategory(int id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, id);

            var ListProduct = await _context.ProductCategory
                .Include(w => w.Products)
                    .ThenInclude(w => w.ProductsPrice)
                .Where(w => w.Products.Any(p => ListProductOnStore.Contains(p.Id)))
                .Select(w => new ProductOnCategory
                {
                    CategoryId = w.Id,
                    CategoryName = w.Name,
                    ListProduct = w.Products
                    .Where(w => w.ProductsPrice.Any(pp => pp.DayWeek == DateTime.Now.DayOfWeek))
                    .Select(p => new ProductList
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.ProductsPrice.Where(w => w.DayWeek == DateTime.Now.DayOfWeek).First().Price,
                        TimeDelivery = p.TimeDelivery.ToString(),
                        Image = p.Image
                    })
                    .ToList()
                })
                .ToListAsync();

            return ListProduct;
        }

        /// <summary>
        /// Method for create new Product
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Product> Post(Product product)
        {
            await _context.Product.AddAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }
    }
}
