using AuFood.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly _DbContext _context;

        public CartController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Cart> Post(Cart cart)
        {
            return cart;
        }
    }
}
