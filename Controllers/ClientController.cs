using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            client.Created = DateTime.Now;
            client.Logo = "https://scontent-gru1-1.xx.fbcdn.net/v/t39.30808-1/326231821_2095207490674732_2282325065885588157_n.jpg?stp=dst-jpg_p320x320&_nc_cat=110&ccb=1-7&_nc_sid=5f2048&_nc_eui2=AeEDj2xIiMVhMDGrfTMvNOPJq1yqdckdFhurXKp1yR0WG15THGtwHBPcD-L0foelBc6bwW_Il6LGdrCq3JAhEArN&_nc_ohc=zORCUanPz3EAX8xL5yK&_nc_ht=scontent-gru1-1.xx&oh=00_AfB6p2QpMAwAt_A_qb0rssH3HX6UDApvoRioEhjANEbNDQ&oe=6558E577";
            
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        [HttpDelete("{client_id}")]
        public async Task<ActionResult<bool>> Delete(int client_id)
        {
            var client_remove = await _context.Client.FindAsync(client_id);

            if(client_remove == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client_remove);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{client_id}")]
        public async Task<ActionResult<Client>> Update(Client client, int client_id)
        {
            var client_update = await _context.Client.FindAsync(client_id);

            if (client_update == null)
            {
                return NotFound();
            }

            client.SerializeProps(ref client_update);

            await _context.SaveChangesAsync();

            return client_update;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<Client>> ListAll()
        {
            var list_all = await _context.Client
                .ToListAsync();

            return list_all;
        }

        [HttpGet("{client_id}")]
        public async Task<ActionResult<Client>> ListAll(int client_id)
        {
            var client = await _context.Client.FindAsync(client_id);

            if(client == null)
            {
                return NotFound();
            }

            return client;
        }
    }
}
