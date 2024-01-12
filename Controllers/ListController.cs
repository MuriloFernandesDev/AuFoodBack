using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var ListStore = await _context.Store
                .Where(w => vStoresID.Contains(w.Id))
                .Select(w => new LabelValue
                {
                    Label = w.Name,
                    Value = w.Id
                })
                .ToListAsync();

            return ListStore;
        }

        [AllowAnonymous]
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
