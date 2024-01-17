using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace AuFood.Controllers
{

    public class Params
    {
        public string? q { get; set; }
    }

    public class ParamsOrderList : Params
    {
        public bool delivery { get; set; }
        public bool pickup { get; set; }
    }
    
    public class product_id
    {
        public int id { get; set; }
        public string? observation { get; set; }
    }

    public class newOrder
    {
        public Order order { get; set; }

        public Consumer consumer { get; set; }

        public Consumer_address consumerAddress { get; set; }

        public int storeId { get; set; }

        public List<product_id> products { get; set; }
    }

    public class ListOrder
    {
        public int qtd { get; set; }
        public OrderStatus status { get; set; }
        public List<Order> Orders { get; set; }
    }
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly _DbContext _context;

        public OrderController(_DbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ListOrder>>> ListOrder([FromQuery] ParamsOrderList pParams)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var vQuery = _context.Order
                .Include(w => w.Store)
                .Include(w => w.Consumer)
                .Include(w => w.Consumer_address)
                .Include(w => w.Order_product)
                    .ThenInclude(w => w.Product)
                    .ThenInclude(w => w.Product_price)
                .Where(w => vStoresID.Contains(w.Store_id))
                .AsQueryable();

            //Filtrar
            if(pParams != null)
            {
                if (!pParams.q.IsNullOrEmpty())
                {
                    vQuery = vQuery
                        .Where(w => !pParams.q.IsNullOrEmpty() ? (w.Consumer.Name.Contains(pParams.q) || w.Id.ToString().Contains(pParams.q)) : true)
                        .AsQueryable();
                }

                if(pParams.delivery == false)
                {
                    vQuery = vQuery
                        .Where(w => w.Delivery_method != DeliveryMethod.Delivery)
                        .AsQueryable();
                }

                if (pParams.pickup == false)
                {
                    vQuery = vQuery
                        .Where(w => w.Delivery_method != DeliveryMethod.Pickup)
                        .AsQueryable();
                }
            }

            var orders = await vQuery
                .GroupBy(w => w.Status)
                .Select(w => new ListOrder
                {
                    qtd = w.Select(w => w).Count(),
                    status = w.Key.Value,
                    Orders = w.Select(o => o).ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult> CancelOrder(int id, Order order_status)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var order = await _context.Order.FindAsync(id);

            if (order == null) 
                return NotFound();

            if (!vStoresID.Contains(order.Store_id))
                return Unauthorized();

            order.Status = order_status.Status;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("total_orders")]
        public async Task<ActionResult<int>> getTotalOrders()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var total_orders = await _context.Order
                .Where(w => vStoresID.Contains(w.Store_id) && w.Status == OrderStatus.Done)
                .CountAsync();

            return total_orders;
        }

        [HttpGet("total_order_product")]
        public async Task<ActionResult<int>> getTotalOrdersProduct()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var total_orders_product = await _context.Order_product
                .Include(w => w.Order)
                .Where(w => vStoresID.Contains(w.Order.Store_id) && w.Order.Status == OrderStatus.Done)
                .CountAsync();

            return total_orders_product;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Order>> Post(newOrder order)
        {
            var ConsumerStore = await _context.Consumer_store
                .Where(w => w.Consumer_id == order.consumer.Id && w.Store_id == order.storeId)
                .FirstOrDefaultAsync();

            var Consumer = await _context.Consumer.FindAsync(order.consumer.Id);
            var ConsumerAddress = await _context.Consumer_address.FindAsync(order.consumerAddress.Id);
            var Store = await _context.Store.FindAsync(order.storeId);

            if (Consumer == null)
            {
                var newConsumer = order.consumer;

                _context.Consumer.Add(newConsumer);

                Consumer = newConsumer;
            }

            if (ConsumerStore == null)
            {
                _context.Consumer_store.Add(new Consumer_store
                {
                    Consumer = Consumer,
                    Store = Store,
                });
            }
            
            if(ConsumerAddress == null)
            {
                var NewConsumerAddress = order.consumerAddress;

                NewConsumerAddress.Consumer = Consumer;

                _context.Consumer_address.Add(NewConsumerAddress);

                ConsumerAddress = NewConsumerAddress;
            }

            var newOrderEntity = new Order
            {
                Consumer = Consumer,
                Consumer_address = ConsumerAddress,
                Store_id = Store.Id,
                Date = DateTime.Now,
                Total_price = 0,
                Order_product = order.products.Select(w => new Order_product
                {
                    Product_id = w.id,
                    Observation = w.observation,
                    Price = _context.Product_price
                        .Where(pp => pp.Day_week == DateTime.Now.DayOfWeek && pp.Product_id == w.id)
                        .Select(w => w.Price)
                        .FirstOrDefault()
                }).ToList()
            };

            newOrderEntity.Total_price = newOrderEntity.Order_product.Sum(w => w.Price);

            _context.Order.Add(newOrderEntity);
            await _context.SaveChangesAsync();

            return newOrderEntity;
        }
    }
}
