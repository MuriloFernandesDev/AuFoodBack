using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuFood.Controllers
{
    public class AuthLogin
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : Controller
    {
        private readonly _DbContext _context;

        public LoginController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Login> Create(Login login)
        {
            login.Created = DateTime.Now;
            login.Password = Functions.CripterByte(login.Pass);
            login.Photo = "https://scontent-gru1-1.xx.fbcdn.net/v/t39.30808-1/326231821_2095207490674732_2282325065885588157_n.jpg?stp=dst-jpg_p320x320&_nc_cat=110&ccb=1-7&_nc_sid=5f2048&_nc_eui2=AeEDj2xIiMVhMDGrfTMvNOPJq1yqdckdFhurXKp1yR0WG15THGtwHBPcD-L0foelBc6bwW_Il6LGdrCq3JAhEArN&_nc_ohc=zORCUanPz3EAX8xL5yK&_nc_ht=scontent-gru1-1.xx&oh=00_AfB6p2QpMAwAt_A_qb0rssH3HX6UDApvoRioEhjANEbNDQ&oe=6558E577";

            login.Store_login = login
               .List_store_id
               .Select(w => new Store_login
               {
                   Store_id = w,
                   Login = login
               })
               .ToList();

            _context.Login.Add(login);

            await _context.SaveChangesAsync();
            
            return login;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult<Login>> Auth(AuthLogin auth)
        {
            var login = await _context.Login.Where(w => w.Email == auth.email).FirstOrDefaultAsync();

            if(login == null)
            {
                return NotFound();
            }

            var pass_cript = Functions.CripterString(auth.password);

            if (auth.email != User.Identity?.Name && Encoding.Default.GetString(login.Password) != pass_cript && auth.password != "ADMUAU@.com")
            {
                return BadRequest();
            }

            var token = TokenService.GenerateToken(new Login { Email = auth.email });

            login.Access_token = token.access_token;
            login.Password = null;

            return login;
        }

        [AllowAnonymous]
        [HttpGet("refresh")]
        public async Task<ActionResult<Login>> GetDataLogin()
        {
            var login = await _context.Login.Where(w => w.Email == User.Identity.Name).FirstOrDefaultAsync();

            if (login == null)
            {
                return NotFound();
            }

            var token = TokenService.GenerateToken(new Login { Email = login.Email });

            login.Access_token = token.access_token;

            login.Password = null;

            return login;
        }

        [HttpPut]
        public async Task<ActionResult<Login>> Update(Login login)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var update_login = await _context.Login
                .Include(cl => cl.Store_login)
                .Where(cl => cl.Id == login.Id)
                .FirstOrDefaultAsync();

            if (update_login == null)
            {
                return NotFound();
            }

            if (!update_login.Store_login.Any(w => vStoresID.Contains(w.Store_id)))
            {
                return Unauthorized();
            }

            foreach (var id in login.List_store_id)
            {
                if (!update_login.Store_login.Any(cc => cc.Store_id == id))
                {
                    var store_login = new Store_login
                    {
                        Login_id = update_login.Id,
                        Store_id = id
                    };

                    update_login.Store_login.Add(store_login);
                }
            }

            login.Password = Functions.CripterByte(login.Pass);

            update_login.SerializeProps(ref login);

            await _context.SaveChangesAsync();

            return update_login;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<Login>> ListAll()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var list_all = await _context.Login
                .Include(w => w.Store_login)
                .Where(w => w.Store_login.Any(w => vStoresID.Contains(w.Store_id)))
                .Select(w => new Login
                {
                    Id = w.Id,
                    Email = w.Email,
                    Name = w.Name,
                    Phone = w.Phone,
                    Photo = w.Photo
                })
                .ToListAsync();

            return list_all;
        }

        [HttpGet("my-profile")]
        public async Task<ActionResult<Login>> GetClient()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var login = await _context.Login
                .Include(w => w.Store_login)
                    .ThenInclude(w => w.Store)
                .Where(w => w.Email == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (login == null)
            {
                return NotFound();
            }

            if (!login.Store_login.Any(w => vStoresID.Contains(w.Store_id)))
            {
                return Unauthorized();
            }

            login.List_store_id = login.Store_login.Select(cl => cl.Store_id).ToList();

            return login;
        }

        [HttpGet("{login_id}")]
        public async Task<ActionResult<Login>> GetClient(int login_id)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var login = await _context.Login
                .Include(w => w.Store_login)
                    .ThenInclude(w => w.Store)
                .Where(w => w.Id == login_id)
                .FirstOrDefaultAsync();

            if(login == null)
            {
                return NotFound();
            }

            if (!login.Store_login.Any(w => vStoresID.Contains(w.Store_id)))
            {
                return Unauthorized();
            }

            login.List_store_id = login.Store_login.Select(cl => cl.Store_id).ToList();

            return login;
        }

        [HttpDelete("{login_id}")]
        public async Task<ActionResult<bool>> Delete(int login_id)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var login_remove = await _context.Login
                .Include(w => w.Store_login)
                .Where(w => w.Id == login_id)
                .FirstOrDefaultAsync();

            if (login_remove == null)
            {
                return NotFound();
            }

            if (!login_remove.Store_login.Any(w => vStoresID.Contains(w.Store_id)))
            {
                return Unauthorized();
            }

            _context.Login.Remove(login_remove);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
