using Microsoft.AspNetCore.Mvc;
using AuFood.Models;
using AuFood.Auxiliary;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipCodeController : Controller
    {
        private readonly _DbContext _context;

        public ZipCodeController(_DbContext context)
        {
            _context = context;
        }

        [HttpGet("address_by_zip_code")]
        public async Task<StoreAddress> GetAddress(int zip_code)
        {
            var Address = await new Connect().GetAddress(zip_code);

            var state = new State
            {
                Name = Address.uf,
            };

            var city = new City
            {
                Name = Address.localidade,
                Abbreviation = "",
                State = state
            };

            var zipCode = new StoreAddress
            {
                Zip = int.Parse(Address.cep),
                Street = Address.logradouro,
                Neighborhood = Address.bairro,
                City = city
            };

            await _context.State.AddAsync(state);
            await _context.City.AddAsync(city);
            await _context.StoreAddress.AddAsync(zipCode);

            await _context.SaveChangesAsync();

            return zipCode;
        }
    }
}
