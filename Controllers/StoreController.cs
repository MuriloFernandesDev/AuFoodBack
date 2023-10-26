using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly _DbContext _context;

        public StoreController(_DbContext context)
        {
            _context = context;
        }
        
        [HttpPost("add/store")]
        public async Task<Store> CreateStore(Store store)
        {
            _context.Store.Add(store);
            await _context.SaveChangesAsync();

            return store;
        }

        [HttpPost("add/storeCategory")]
        public async Task<StoreCategory> CreateStoreCategory(StoreCategory storeCategory)
        {
            _context.StoreCategory.Add(storeCategory);

            await _context.SaveChangesAsync();
            return storeCategory;
        }

        [HttpGet("{name}")]
        public async Task<Store> GetStoreByName(string name)
        {
            //remover todo espaço de w.Name e name e colocar -
            var Store = await _context.Store
                .Include(w => w.City)
                    .ThenInclude(w => w.State)
                .Where(w => w.Name.Replace(" ", "-").ToLower() == name.Replace(" ", "-").ToLower())
                .SingleOrDefaultAsync();

            return Store;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<Store>> GetListStoreAll()
        {
            //remover todo espaço de w.Name e name e colocar -
            var Store = await _context.Store
                .Include(w => w.City)
                    .ThenInclude(w => w.State)
                .Select(w => new Store
                {
                    Name = w.Name,
                    Logo = w.Logo
                })
                .ToListAsync();

            return Store;
        }
    }
}
