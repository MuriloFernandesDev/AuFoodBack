using AuFood.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : Controller
    {

        private readonly _DbContext _context;

        public ProductCategoryController(_DbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ProductCategory> Post(ProductCategory productCategory)
        {
            await _context.ProductCategory.AddAsync(productCategory);

            await _context.SaveChangesAsync();

            return productCategory;
        }
    }
}
