
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;

namespace BlazingPizza.Controllers
{
    [Route("specials")]
    [ApiController]
    public class SpecialsController : Controller
    {
        private readonly PizzaStoreContext _db;

        public SpecialsController(PizzaStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
        {
            // todo: we should use a DTO to avoid overexposing our data model
            // also create a extra layer to map between the data model and the DTO
            // use AutoMapper or similar library
            // use a service layer to handle business logic
            return (await _db.Specials.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
        }
    }
}