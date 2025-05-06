using Ecommerce.API.Data;
using Ecommerce.Shared.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        public readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }


        [HttpPut]
        public async Task<ActionResult> Put(City city)
        {
            _context.Update(city);
            await _context.SaveChangesAsync();
            return Ok(city);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var afectedRows = await _context.Cities.Where(c => c.Id == id).ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(City city)
        {
            _context.Add(city);
            await _context.SaveChangesAsync();
            return Ok(city);
        }
    }
}
