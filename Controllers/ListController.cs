using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    public class LabelValue
    {
        public int Value { get; set; }

        public string Label { get; set; }

        public int? ClientId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ListController : Controller
    {
        private readonly _DbContext _context;

        public ListController(_DbContext context)
        {
            _context = context;
        }

        [HttpGet("store")]
        public async Task<IEnumerable<LabelValue>> ListStore()
        {
            var ListStore = await _context.Store
                .Select(w => new LabelValue
                {
                    Label = w.Name,
                    Value = w.Id
                })
                .ToListAsync();

            return ListStore;
        }

        [HttpGet("productCategory")]
        public async Task<IEnumerable<LabelValue>> ListProductCategory()
        {
            var ListStore = await _context.Product_category
                .Select(w => new LabelValue
                {
                    Label = w.Name,
                    Value = w.Id
                })
                .ToListAsync();

            return ListStore;
        }
    }
}
