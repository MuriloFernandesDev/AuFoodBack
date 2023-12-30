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

    public class ListOrder : Order
    {
        public List<Product> Products { get; set; }
        public string Consumer_name { get; set; }
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

        [HttpGet("dash/list")]
        public async Task<ActionResult<ListOrder>> ListOrder()
        {
            var orders = await _context.Order
                .Include(w => w.Store)
                .Include(w => w.Consumer)
                .Include(w => w.ConsumerAddress)
                .Include(w => w.OrderProduct)
                    .ThenInclude(w => w.Product)
                    .ThenInclude(w => w.ProductsPrice)
                .Select(w => new ListOrder
                {
                    Id = w.Id,
                    Consumer_name = w.Consumer.Name,
                    Date = w.Date,
                    Products = w.OrderProduct.Select(op => new Product
                    {
                        Name = op.Product.Name,
                        Price = op.Price
                    }).ToList(),
                    StoreId = w.StoreId,
                    TotalPrice = w.TotalPrice,
                    Status = w.Status
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPut("dash/cancel/{id}")]
        public async Task<ActionResult> CancelOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null) 
                return NotFound();

            order.Status = OrderStatus.Canceled;

            await _context.SaveChangesAsync();

            return Ok();
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
                var NewConsumerAddress = order.consumerAddress;

                NewConsumerAddress.Consumer = Consumer;

                _context.ConsumerAddress.Add(NewConsumerAddress);

                ConsumerAddress = NewConsumerAddress;
            }
            
            var newOrder = order.order;

            newOrder.Consumer = Consumer;
            newOrder.ConsumerAddress = ConsumerAddress;
            newOrder.StoreId = Store.Id;
            newOrder.Date = DateTime.Now;
            newOrder.TotalPrice = 0;

            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();

            var OrderProduct = order.products.Select(w => new OrderProduct
            {
                OrderId = newOrder.Id,
                ProductId = w.id,
                Quantity = w.quantity,
                Price = _context.ProductPrice.Find(w.id)?.Price ?? 0
            }).ToList();

            _context.OrderProduct.AddRange(OrderProduct);

            newOrder.TotalPrice = OrderProduct.Sum(w => w.Price);

            await _context.SaveChangesAsync();
            return newOrder;
        }
    }
}
