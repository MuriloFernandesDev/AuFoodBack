using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuFood.Controllers
{

    public class StoreListAll : Store
    {
        public string Rating { get; set; }

        public int QtdRating { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly _DbContext _context;

        public StoreController(_DbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<Store> CreateStore(Store store)
        {
            _context.Store.Add(store);
            await _context.SaveChangesAsync();

            return store;
        }

        [HttpPost("storeCategory")]
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
                .Include(w => w.ZipCode)
                    .ThenInclude(w => w.City)
                    .ThenInclude(w => w.State)
                .Where(w => w.Name.Replace(" ", "-").ToLower() == name.Replace(" ", "-").ToLower())
                .SingleOrDefaultAsync();

            return Store;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<StoreListAll>> GetListStoreAll()
        {
            //remover todo espaço de w.Name e name e colocar -
            var Store = await _context.Store
                .Include(w => w.AvaliationsStories)
                .Include(w => w.ZipCode)
                    .ThenInclude(w => w.City)
                    .ThenInclude(w => w.State)
                .ToListAsync();

            var StoreListAll = Store.Select(w => new StoreListAll
            {
                Logo = w.Logo,
                Name = w.Name,
                Rating = w.AvaliationsStories.Any() ? (w.AvaliationsStories.Sum(a => a.Rating) / w.AvaliationsStories.Count).ToString("0.0") : "0.0",
                QtdRating = w.AvaliationsStories.Count
            }).ToList();

            return StoreListAll;
        }

        [HttpGet("dash/list_all")]
        public async Task<IEnumerable<Store>> ListAll()
        {
            var list_all = await _context.Store
                .ToListAsync();

            return list_all;
        }

        [HttpGet("avaliation/{id}")]
        public async Task<int> GetAvalationStore(int id)
        {
            //buscar lista de avaliacoes por store id e fazer a média
            var Avaliation = await _context.AvaliationStore
                .Where(w => w.StoreId == id)
                .Select(w => w.Rating)
                .ToListAsync();

            return Avaliation.Sum() / Avaliation.Count;
        }
    }
}
