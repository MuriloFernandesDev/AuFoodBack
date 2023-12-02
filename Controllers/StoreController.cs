using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        public async Task<IEnumerable<StoreListAll>> GetListStoreAll()
        {
            //remover todo espaço de w.Name e name e colocar -
            var Store = await _context.Store
                .Include(w => w.AvaliationsStories)
                .Include(w => w.City)
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


        [HttpPost]
        public async Task<Store> CreateStore(Store store)
        {
            store.BackgroundImage = "https://images3.alphacoders.com/131/1313839.jpg";
            store.Logo = "https://img.freepik.com/vetores-premium/vetor-do-logotipo-do-burger-art-design_260747-237.jpg";

            _context.Store.Add(store);
            await _context.SaveChangesAsync();

            return store;
        }

        [HttpPut("{store_id}")]
        public async Task<ActionResult<Store>> CreateStore(Store newStore, int store_id)
        {
            var store = await _context.Store.FindAsync(store_id);

            newStore.BackgroundImage = "https://images3.alphacoders.com/131/1313839.jpg";
            newStore.Logo = "https://img.freepik.com/vetores-premium/vetor-do-logotipo-do-burger-art-design_260747-237.jpg";

            if (store == null)
            {
                return NotFound();
            }

            newStore.SerializeProps(ref store);

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

        [HttpGet("dash/list_all")]
        public async Task<IEnumerable<Store>> ListAll()
        {
            var list_all = await _context.Store
                .ToListAsync();

            return list_all;
        }

        [HttpGet("dash/{store_id}")]
        public async Task<Store> GetStoreByName(int store_id)
        {
            //remover todo espaço de w.Name e name e colocar -
            var Store = await _context.Store
                .Include(w => w.City)
                    .ThenInclude(w => w.State)
                .Where(w => w.Id == store_id)
                .SingleOrDefaultAsync();

            return Store;
        }
    }
}
