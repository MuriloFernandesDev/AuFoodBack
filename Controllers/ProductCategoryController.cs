using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<Models.Product_category> Post(Models.Product_category productCategory)
        {
            await _context.Product_category.AddAsync(productCategory);

            await _context.SaveChangesAsync();

            return productCategory;
        }

        [HttpGet("list_all")]
        public async Task<List<Models.Product_category>> ListProductCategory()
        {
            var listCategory = await _context.Product_category.ToListAsync();
            return listCategory;
        }

        /// <summary>
        /// Get list all Product Category
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_categories_store/{store_id}")]
        public async Task<List<Models.Product_category>> Post(int store_id)
        {
            var ListProductOnStore = await ProductAux.GetAllProductOnStore(_context, store_id);

            var listCategories = await _context.Product_category
                .Include(w => w.Product)
                .Where(w => w.Product.Any(p => ListProductOnStore.Contains(p.Id)) && w.Product.Count > 0)
                .ToListAsync();

            return listCategories;
        }
    }
}
