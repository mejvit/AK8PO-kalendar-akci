using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MejvitCalendarBackend.Controllers
{
    [ApiController]
    public class EventCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventCategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("api/categories")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _context.Categories.OrderBy(p => p.Name).ToListAsync();
            return Ok(categories);
        }


        [Route("api/categories")]
        [HttpPost]
        public async Task<IActionResult> Post(EventCategory category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return Ok(category.Id);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }


        [Route("api/categories/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
