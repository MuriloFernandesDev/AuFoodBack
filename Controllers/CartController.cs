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
        public int quantity { get; set; }
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
            newCart.TotalPrice = 0;

            _context.Cart.Add(newCart);
            await _context.SaveChangesAsync();

            //var cartProductsToAdd = new List<CartProduct>();

            //foreach (var product in cart.products)
            //{
            //    var ProductDB = await _context.Product
            //        .Include(w => w.ProductsPrice)
            //        .Where(w => w.Id == product.id)
            //        .FirstOrDefaultAsync();

            //    var CartProductExisting = cartProductsToAdd
            //        .FirstOrDefault(w => w.ProductId == product.id && w.CartId == newCart.Id);

            //    if(CartProductExisting != null)
            //    {
            //        CartProductExisting.Quantity += 1;
            //    }
            //    else
            //    {
            //        var cartProduct = new CartProduct
            //        {
            //            CartId = newCart.Id,
            //            ProductId = ProductDB.Id,
            //            Quantity = 1
            //        };

            //        cartProductsToAdd.Add(cartProduct);
            //    }

            //    // Atualize o preço total do carrinho aqui, se necessário
            //    newCart.TotalPrice += ProductDB.ProductsPrice
            //        .Where(w => w.DayWeek == DateTime.Now.DayOfWeek)
            //        .Select(w => w.Price)
            //        .FirstOrDefault();
            //}

            _context.CartProduct.AddRange(cart.products.Select(w => new CartProduct
            {
                CartId = newCart.Id,
                ProductId = w.id,
                Quantity = w.quantity
            }).ToList());
            await _context.SaveChangesAsync();

            return newCart;
        }
    }
}
