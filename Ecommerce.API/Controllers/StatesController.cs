using Ecommerce.API.Data;
using Ecommerce.Shared.Entitites;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StatesController : Controller
    {

        public readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }


        [HttpPut]
        public async Task<ActionResult> Put(State state){
            _context.Update(state);
            await _context.SaveChangesAsync();
            return Ok(state);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            var afectedRows = await _context.States.Where(c => c.Id == id).ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _context.States.Include(c => c.Cities).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(State state)
        {
            _context.Add(state);
            await _context.SaveChangesAsync();
            return Ok(state);
        }


    }
}
