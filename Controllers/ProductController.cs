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
    }

    public class ProductOnCategory
    {
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public List<ProductList> ListProduct { get; set; }
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

        /*
         * Pega uma lista de todos os produtos que a loja possui permissão
         */
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
                    productCategory = p.ProductCategory
                })
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
                        TimeDelivery = p.TimeDelivery.ToString()
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
