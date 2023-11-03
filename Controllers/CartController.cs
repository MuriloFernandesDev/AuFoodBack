using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuFood.Controllers
{

    public class product_id
    {
        public int id { get; set; }
    }

    public class NewCart
    {
        public DeliveryMethod deliveryMethod { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public Consumer consumer { get; set; }

        public List<product_id> products { get; set; }
    }
    
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
        public async Task<NewCart> Post(NewCart cart)
        {
            return cart;
        }
    }
}
