using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
using MejvitCalendarBackend.Models.Entities;
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
                EventCategoryEntity eventCategoryEntity = new EventCategoryEntity()
                {
                    Code = category.Code,
                    Name = category.Name
                };
                _context.Categories.Add(eventCategoryEntity);
                await _context.SaveChangesAsync();
                return Ok(eventCategoryEntity.Id);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Route("api/categories/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] EventCategory updatedCategory)
        {
            if (updatedCategory == null || updatedCategory.Id != id)
            {
                return BadRequest();
            }

            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Entry<EventCategoryEntity>(categoryEntity).CurrentValues.SetValues(updatedCategory);
            await _context.SaveChangesAsync();

            return new NoContentResult();
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
