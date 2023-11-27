using Microsoft.AspNetCore.Mvc;
using AuFood.Models;
using AuFood.Auxiliary;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreAddressController : Controller
    {
        private readonly _DbContext _context;

        public StoreAddressController(_DbContext context)
        {
            _context = context;
        }

        [HttpGet("address_by_zip_code")]
        public async Task<StoreAddress> GetAddress(string zip_code)
        {
            var StoreAddress = await _context.StoreAddress.Where(w => w.Zip == zip_code).FirstOrDefaultAsync();

            if(StoreAddress != null)
            {
                return StoreAddress;
            }

            var Address = await new Connect().GetAddress(zip_code);

            var state = await _context.State.Where(s => s.Uf == Address.uf).FirstOrDefaultAsync();
            var city = await _context.City.Where(c => c.Name == Address.localidade).FirstOrDefaultAsync();

            if (state == null)
            {
                state = new State
                {
                    Name = Address.uf,
                    Uf = Address.uf
                };
            }

            if(city == null)
            {
                city = new City
                {
                    Name = Address.localidade,
                    Abbreviation = "",
                    State = state
                };
            }

            StoreAddress = new StoreAddress
            {
                Zip = zip_code,
                Street = Address.logradouro,
                Neighborhood = Address.bairro,
                City = city
            };

            await _context.State.AddAsync(state);
            await _context.City.AddAsync(city);
            await _context.StoreAddress.AddAsync(StoreAddress);

            await _context.SaveChangesAsync();

            return StoreAddress;
        }
    }
}
