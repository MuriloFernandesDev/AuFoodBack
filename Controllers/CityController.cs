using Microsoft.AspNetCore.Mvc;
using AuFood.Models;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("address_by_zip_code")]
        public async Task<ZipCode> GetAddress(int zip_code)
        {
            return new ZipCode();
        }
    }
}
