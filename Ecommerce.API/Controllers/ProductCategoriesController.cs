using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/productscategory")]
    public class ProductCategoriesController : Controller
    {
        private readonly DataContext _context;

        public ProductCategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {

            try
            {
                return Ok(await _context.ProductCategories.Include(p => p.Products).Include(p => p.Categories).ToListAsync());
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
                var afectedRows = await _context.ProductCategories.Where(p => p.Id == 0).ExecuteDeleteAsync();

                if (afectedRows == 0) {

                    return NotFound();

                }
                return NoContent();
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductCategory productCategory) {

            try
            {
                _context.ProductCategories.Add(productCategory);
                await _context.SaveChangesAsync();
                return Ok(productCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(ProductCategory productCategory){

            try 
            {
                _context.ProductCategories.Update(productCategory);
                await _context.SaveChangesAsync();
                return Ok(productCategory);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
