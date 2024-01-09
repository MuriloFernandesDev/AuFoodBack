using Microsoft.AspNetCore.Mvc;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : Controller
    {
        private readonly _DbContext _context;

        public CityController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<City> Create(City city)
        {
            _context.City.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }
    }
}
