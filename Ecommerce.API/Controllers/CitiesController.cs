using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
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
            try 
            {    
                _context.Update(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var afectedRows = await _context.Cities.Where(c => c.Id == id).ExecuteDeleteAsync();

                if (afectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                return Ok(await _context.Cities.ToListAsync());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(City city)
        {
            try
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            } 
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message); 
            }
        }
    }
}
