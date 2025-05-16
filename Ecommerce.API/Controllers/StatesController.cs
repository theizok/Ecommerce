using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
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
            try
            {
                _context.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            try
            {
                var afectedRows = await _context.States.Where(c => c.Id == id).ExecuteDeleteAsync();

                if (afectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                return Ok(await _context.States.Include(c => c.Cities).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> Post(State state)
        {
            try
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
