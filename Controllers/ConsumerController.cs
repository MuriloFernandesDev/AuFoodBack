using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Authorize]
    public class ConsumerController : Controller
    {
        private readonly _DbContext _context;

        public ConsumerController(_DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list all Consumer from Store
        /// </summary>
        /// <param name="store_id">ID from Store</param>
        /// <returns></returns>
        [HttpGet("list_consumer_by_store/{store_id}")]
        public async Task<List<Consumer>> GetListConsumerByStore(int store_id)
        {
            var list_consumer_by_store = await _context.Consumer_store
                .Include(w => w.Consumer)
                .Where(w => w.Store_id == store_id)
                .Select(w => w.Consumer)
                .ToListAsync();

            return list_consumer_by_store;
        }

        /// <summary>
        /// Get list all Consumer from Store
        /// </summary>
        /// <param name="store_id">ID from Store</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get_consumer_by_phone/{phone}")]
        public async Task<ActionResult<Consumer>> GetConsumer(string phone)
        {
            var consumer = await _context.Consumer
                .Where(w => w.Phone == phone && w.Phone_confirmed == true)
                .OrderBy(w => w.Id)
                .Select(w => new Consumer
                {
                    Id = w.Id,
                    Name = w.Name,
                    Phone = w.Phone,
                    Email = w.Email
                })
                .FirstOrDefaultAsync();

            if(consumer == null)
            {
                return NotFound();
            }

            return consumer;
        }

        /// <summary>
        /// Search for the consumer by ID and send the code to the phone
        /// </summary>
        /// <param name="consumer_id">ID from Consumer</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("confirm_consumer/{consumer_id}")]
        public async Task<ActionResult<bool>> ConfirmConsumer(int consumer_id)
        {
            var consumer = await _context.Consumer.FindAsync(consumer_id);

            // Atribui código para o usuário para a próxima verificação
            consumer.Code = new Random().Next(10000).ToString("D4");
            await _context.SaveChangesAsync();

            //enviar o codigo para o consumidor e retornar true

            return true;
        }

        /// <summary>
        /// Confirm the consumer code
        /// </summary>
        /// <param name="consumer_id">ID from Consumer</param>
        /// <param name="code">Code</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("confirm_consumer_code/{consumer_id}/{code}")]
        public async Task<ActionResult<Consumer>> ConfirmConsumer(int consumer_id, string code)
        {
            var consumer = await _context.Consumer
                .Include(w => w.Consumer_address)
                    .ThenInclude(w => w.City)
                .Where(w => w.Id == consumer_id)
                .FirstOrDefaultAsync();

            if(consumer == null)
            {
                return NotFound();
            }

            if(consumer.Code == code)
            {
                return consumer;
            }
            else
            {
                return BadRequest();
            }
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

            newConsumer.Consumer_store = new List<Consumer_store>
                {
                  new Consumer_store {
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
