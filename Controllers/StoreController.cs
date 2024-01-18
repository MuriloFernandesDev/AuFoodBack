using AuFood.Auxiliary;
using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class StoreController : Controller
    {
        private readonly _DbContext _context;

        public StoreController(_DbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("search/{name}")]
        public async Task<ActionResult<Store>> GetStoreByName(string name)
        {
            var Store = await _context.Store
                .Include(w => w.City)
                    .ThenInclude(w => w.State)
                .Where(w => w.Name.Replace(" ", "-").ToLower() == name.Replace(" ", "-").ToLower())
                .SingleOrDefaultAsync();

            if (Store == null)
                return NotFound();

            Store.Views += 1;

            await _context.SaveChangesAsync();

            return Store;
        }

        [AllowAnonymous]
        [HttpGet("list_all_store")]
        public async Task<IEnumerable<StoreListAll>> GetListStoreAll()
        {
            var Store = await _context.Store
                .Include(w => w.City)
                    .ThenInclude(w => w.State)
                .ToListAsync();

            var StoreListAll = Store.Select(w => new StoreListAll
            {
                Logo = w.Logo,
                Name = w.Name
            }).ToList();
             
            return StoreListAll;
        }

        [HttpGet("views")]
        public async Task<ActionResult<int>> GetQuantityViews()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var store_views = await _context.Store
                .Where(w => vStoresID.Contains(w.Id))
                .Select(w => w.Views)
                .ToListAsync();

            return store_views.Sum(w => w) ?? 0;
        }

        [HttpPost]
        public async Task<Store> CreateStore(Store store)
        {
            store.Background_image = "https://images3.alphacoders.com/131/1313839.jpg";
            store.Logo = "https://img.freepik.com/vetores-premium/vetor-do-logotipo-do-burger-art-design_260747-237.jpg";

            _context.Store.Add(store);
            await _context.SaveChangesAsync();

            return store;
        }

        [HttpPut]
        public async Task<ActionResult<Store>> UpdateStore(Store newStore)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, null);

            var store = await _context.Store.FindAsync(newStore.Id);

            if (store == null)
                return NotFound();

            if (!vStoresID.Contains(store.Id))
                return Unauthorized(new { msg = "Não possui permissão para essa loja." });

            newStore.Background_image = "https://images3.alphacoders.com/131/1313839.jpg";
            newStore.Logo = "https://img.freepik.com/vetores-premium/vetor-do-logotipo-do-burger-art-design_260747-237.jpg";

            newStore.SerializeProps(ref store);

            await _context.SaveChangesAsync();

            return store;
        }

        [HttpGet("list_all")]
        public async Task<IEnumerable<Store>> ListAll()
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, Request.HeaderStoreId());

            var list_all = await _context.Store
                .Where(w => vStoresID.Contains(w.Id))
                .ToListAsync();

            return list_all;
        }

        [HttpGet("{store_id}")]
        public async Task<ActionResult<Store>> GetStore(int store_id)
        {
            //Get Stores id Permission
            var vStoresID = await Functions.getStores(_context, User.Identity.Name, null);

            var Store = await _context.Store
                .Include(w => w.City)
                    .ThenInclude(w => w.State)
                .Where(w => w.Id == store_id)
                .SingleOrDefaultAsync();

            if(Store == null)
                return NotFound();

            if (!vStoresID.Contains(Store.Id))
                return Unauthorized();

            return Store;
        }
    }
}
