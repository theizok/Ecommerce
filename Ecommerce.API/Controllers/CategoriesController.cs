using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        public readonly DataContext _context;

        public CategoriesController(DataContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category){
            try 
            {
                 _context.Add(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id) {
            try
            {
                return Ok(await _context.Categories.Include(c => c.ProductCategory).ToListAsync());

            }
            catch (Exception ex) 
            {
             return BadRequest(ex.Message); 
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(Category category) {
            try 
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id) {
            try
            {
                var afectedRows = await _context.Categories.Where(c => c.Id == id).ExecuteDeleteAsync();

                if (afectedRows == 0) {
                    return NotFound();
                }
                return NoContent();
            }
            catch 
            (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
