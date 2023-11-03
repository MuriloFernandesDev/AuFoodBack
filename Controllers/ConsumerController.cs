using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuFood.Controllers
{

    public class NewConsumer : Consumer
    {
        public int StoreId { get; set; }
    }

    [Route("api/[controller]")]
    public class ConsumerController : Controller
    {
        private readonly _DbContext _context;

        public ConsumerController(_DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list all Consumer in Store
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("list_consumer_by_store/{store_id}")]
        public async Task<List<Consumer>> GetListConsumerByStore(int store_id)
        {
            var list_consumer_by_store = await _context.ConsumerStore
                .Include(w => w.Consumer)
                .Where(w => w.StoreId == store_id)
                .Select(w => w.Consumer)
                .ToListAsync();

            return list_consumer_by_store;
        }

        /// <summary>
        /// Get list all Consumer in Store
        /// </summary>
        /// <param name="store_id">ID for Store</param>
        /// <returns></returns>
        [HttpGet("get_consumer_by_phone/{phone}")]
        public async Task<ActionResult<Consumer>> GetConsumer(string phone)
        {
            var consumer = await _context.Consumer
                .Include(w => w.ConsumerAddress)
                .Where(w => w.Phone == phone && !string.IsNullOrEmpty(w.Password.ToString()))
                .OrderBy(w => w.Id)
                .FirstOrDefaultAsync();

            if(consumer == null)
            {
                return NotFound();
            }

            return consumer;
        }

        /// <summary>
        /// Create new Consumer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Consumer> Post(NewConsumer consumer)
        {
            var newConsumer = consumer;

            var Store = await _context.Store.FindAsync(consumer.StoreId);

            newConsumer.ConsumerStore = new List<ConsumerStore>
                {
                  new ConsumerStore {
                    Consumer = newConsumer,
                    Store = Store,
                  }
                };

            await _context.Consumer.AddAsync(newConsumer);
            await _context.SaveChangesAsync();

            return newConsumer;
        }
    }
}
