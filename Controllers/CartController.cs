using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{

    public class product_id
    {
        public int id { get; set; }
    }

    public class NewCart
    {
        public Cart cart { get; set; }

        public Consumer consumer { get; set; }

        public ConsumerAddress consumerAddress { get; set; }

        public int storeId { get; set; }

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
        public async Task<ActionResult<Cart>> Post(NewCart cart)
        {
            var ConsumerStore = await _context.ConsumerStore
                .Where(w => w.ConsumerId == cart.consumer.Id && w.StoreId == cart.storeId)
                .FirstOrDefaultAsync();

            var Consumer = await _context.Consumer.FindAsync(cart.consumer.Id);
            var ConsumerAddress = await _context.ConsumerAddress.FindAsync(cart.consumerAddress.Id);
            var Store = await _context.Store.FindAsync(cart.storeId);

            if (Consumer == null)
            {
                var newConsumer = cart.consumer;

                _context.Consumer.Add(newConsumer);

                Consumer = newConsumer;
            }

            if (ConsumerStore == null)
            {
                _context.ConsumerStore.Add(new ConsumerStore
                {
                    Consumer = Consumer,
                    Store = Store,
                });
            }
            
            if(ConsumerAddress == null)
            {
                var NewConsumerAddress = ConsumerAddress;

                NewConsumerAddress.ConsumerId = Consumer.Id;

                _context.ConsumerAddress.Add(NewConsumerAddress);
            }
            
            var newCart = cart.cart;

            newCart.ConsumerId = Consumer.Id;
            newCart.ConsumerAddressId = ConsumerAddress.Id;
            newCart.StoreId = Store.Id;
            newCart.Date = DateTime.Now;

            _context.Cart.Add(newCart);

            await _context.SaveChangesAsync();

            return newCart;
        }
    }
}
