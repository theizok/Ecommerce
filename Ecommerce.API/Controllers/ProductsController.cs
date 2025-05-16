using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        public readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductCategory productCategory) {
            try
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
                return Ok(productCategory);
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
                var afectedRows = await _context.ProductCategories.Where(x => x.Id == id).ExecuteDeleteAsync();

                if (afectedRows == 0) {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Fallo al eliminar {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(ProductCategory productCategory) {
                try 
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                    return Ok(productCategory);
                } 
                catch (Exception ex)
                {
                    return BadRequest($"Error al actualizar {ex.Message}");
                }
        }


        [HttpGet]
        public async Task<ActionResult> Get() {
            try 
            {
                return Ok(await _context.Products.Include(p => p.ProductCategory).ToListAsync());
            } 
            catch (Exception ex) 
            {
                return BadRequest($"Error al realizar la peticion get {ex.Message}");
            }
        }

    }
}
