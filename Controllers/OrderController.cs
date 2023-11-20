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

    public class newOrder
    {
        public Order order { get; set; }

        public Consumer consumer { get; set; }

        public ConsumerAddress consumerAddress { get; set; }

        public int storeId { get; set; }

        public List<product_id> products { get; set; }
    }
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly _DbContext _context;

        public OrderController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(newOrder order)
        {
            var ConsumerStore = await _context.ConsumerStore
                .Where(w => w.ConsumerId == order.consumer.Id && w.StoreId == order.storeId)
                .FirstOrDefaultAsync();

            var Consumer = await _context.Consumer.FindAsync(order.consumer.Id);
            var ConsumerAddress = await _context.ConsumerAddress.FindAsync(order.consumerAddress.Id);
            var Store = await _context.Store.FindAsync(order.storeId);

            if (Consumer == null)
            {
                var newConsumer = order.consumer;

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
            
            var newOrder = order.order;

            newOrder.ConsumerId = Consumer.Id;
            newOrder.ConsumerAddressId = ConsumerAddress.Id;
            newOrder.StoreId = Store.Id;
            newOrder.Date = DateTime.Now;
            newOrder.TotalPrice = 0;

            _context.Order.Add(newOrder);
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

            _context.OrderProduct.AddRange(order.products.Select(w => new OrderProduct
            {
                OrderId = newOrder.Id,
                ProductId = w.id,
                Quantity = w.quantity
            }).ToList());
            
            await _context.SaveChangesAsync();
            return newOrder;
        }
    }
}
