using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly _DbContext _context;

        public ProductController(_DbContext context)
        {
            _context = context;
        }
        
        [HttpGet("list")]
        public async Task<IEnumerable<Product>> GetListProduct()
        {
            var ListProduct = await _context.Product
                .Include(w => w.ProductCategory)
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
