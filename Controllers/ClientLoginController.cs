using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientLoginController : Controller
    {
        private readonly _DbContext _context;

        public ClientLoginController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ClientLogin> Create(ClientLogin clientLogin)
        {
            clientLogin.Created = DateTime.Now;
            clientLogin.Password = Functions.CripterByte(clientLogin.Pass);
            clientLogin.Photo = "https://scontent-gru1-1.xx.fbcdn.net/v/t39.30808-1/326231821_2095207490674732_2282325065885588157_n.jpg?stp=dst-jpg_p320x320&_nc_cat=110&ccb=1-7&_nc_sid=5f2048&_nc_eui2=AeEDj2xIiMVhMDGrfTMvNOPJq1yqdckdFhurXKp1yR0WG15THGtwHBPcD-L0foelBc6bwW_Il6LGdrCq3JAhEArN&_nc_ohc=zORCUanPz3EAX8xL5yK&_nc_ht=scontent-gru1-1.xx&oh=00_AfB6p2QpMAwAt_A_qb0rssH3HX6UDApvoRioEhjANEbNDQ&oe=6558E577";

            clientLogin.Client_ClientLogin = clientLogin
                .ListClientsId
                .Select(w => new Client_ClientLogin 
                {
                    ClientId = w,
                    ClientLogin = clientLogin
                })
                .ToList();
        
            _context.ClientLogin.Add(clientLogin);

            await _context.SaveChangesAsync();
            
            return clientLogin;
        }

        [HttpPut("{client_login_id}")]
        public async Task<ActionResult<ClientLogin>> Update(ClientLogin ClientLogin, int client_login_id)
        {
            var client_update = await _context.ClientLogin
                .Include(cl => cl.Client_ClientLogin)
                .FirstOrDefaultAsync(cl => cl.Id == client_login_id);

            if (client_update == null)
            {
                return NotFound();
            }

            ClientLogin.SerializeProps(ref client_update);

            foreach (var clientId in ClientLogin.ListClientsId)
            {
                if (!client_update.Client_ClientLogin.Any(cc => cc.ClientId == clientId))
                {
                    var clientClientLogin = new Client_ClientLogin
                    {
                        ClientId = clientId,
                        ClientLogin = client_update
                    };

                    client_update.Client_ClientLogin.Add(clientClientLogin);
                }
            }

            await _context.SaveChangesAsync();

            return client_update;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<ClientLogin>> ListAll()
        {
            var list_all = await _context.ClientLogin
                .Include(w => w.Client)
                .Select(w => new ClientLogin
                {
                    Id = w.Id,
                    Client = w.Client,
                    Email = w.Email,
                    Name = w.Name,
                    Phone = w.Phone,
                    Photo = w.Photo
                })
                .ToListAsync();

            return list_all;
        }

        [HttpGet("{client_login_id}")]
        public async Task<ActionResult<ClientLogin>> GetClient(int client_login_id)
        {
            var clientLogin = await _context.ClientLogin
                .Include(w => w.Client)
                .Include(w => w.Client_ClientLogin)
                    .ThenInclude(w => w.Client)
                .Where(w => w.Id == client_login_id)
                .FirstOrDefaultAsync();

            if(clientLogin == null)
            {
                return NotFound();
            }

            clientLogin.ListClientsId = clientLogin.Client_ClientLogin.Select(cl => cl.Client.Id).ToList();

            return clientLogin;
        }

        [HttpDelete("{client_login_id}")]
        public async Task<ActionResult<bool>> Delete(int client_login_id)
        {
            var client_login_remove = await _context.ClientLogin.FindAsync(client_login_id);

            if (client_login_remove == null)
            {
                return NotFound();
            }

            _context.ClientLogin.Remove(client_login_remove);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
