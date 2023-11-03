using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Create new Product Category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProductCategory> Post(ProductCategory productCategory)
        {
            await _context.ProductCategory.AddAsync(productCategory);

            await _context.SaveChangesAsync();

            return productCategory;
        }

        /// <summary>
        /// Get list all Product Category
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_categories_store/{store_id}")]
        public async Task<List<ProductCategory>> Post(int store_id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);

            var listCategories = await _context.ProductCategory
                .Include(w => w.Products)
                .Where(w => w.Products.Any(p => ListProductOnStore.Contains(p.Id)) && w.Products.Count > 0)
                .ToListAsync();

            return listCategories;
        }
    }
}
