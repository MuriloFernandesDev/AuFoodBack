using AuFood.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly _DbContext _context;

        public ClientController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Client> Create(Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}
