using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : Controller
    {

        private readonly DataContext _context;

        public CountriesController(DataContext context) 
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Country country, int id) {
            try
            {
                //Se busca el pais a actualizar su nombre
                var countryToUpdate = await _context
                     .Countries
                     .FirstOrDefaultAsync(countryF => countryF.Id == id);

                //Si no se encuentra nada se devuelve NotFound
                if (countryToUpdate == null)
                {
                    return NotFound();
                }
                else
                {
                    countryToUpdate.Name = country.Name;

                    _context.Update(countryToUpdate);

                    await _context.SaveChangesAsync();
                    return Ok(countryToUpdate);
                }


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
                var afectedRows = await _context.Countries.Where(c => c.Id == id).ExecuteDeleteAsync();

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
                return Ok(await _context.Countries.Include(c => c.States).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> Post(Country country) {

            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
