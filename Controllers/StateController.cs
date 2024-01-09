using AuFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StateController : Controller
    {
        private readonly _DbContext _context;

        public StateController(_DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<State> Create(State state)
        {
            _context.State.Add(state);
            await _context.SaveChangesAsync();
            return state;
        }
    }
}
