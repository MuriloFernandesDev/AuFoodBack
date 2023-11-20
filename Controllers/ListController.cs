using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    public class LabelValue
    {
        public int Value { get; set; }

        public string Label { get; set; }
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

        [HttpGet("clients")]
        public async Task<IEnumerable<LabelValue>> ListClients()
        {
            var ListClients = await _context.Client
                .Select(w => new LabelValue
                {
                    Label = w.Name,
                    Value = w.Id
                })
                .ToListAsync();

            return ListClients;
        }
    }
}
