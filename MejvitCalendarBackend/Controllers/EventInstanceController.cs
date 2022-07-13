using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
using MejvitCalendarBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MejvitCalendarBackend.Controllers
{
    [Route("api/event-instances")]
    [ApiController]
    public class EventInstanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventInstanceController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var places = await _context.Places.OrderBy(p => p.Name).ToListAsync();
            var eventInstances = await _context
                .EventInstances
                .Where(eventInstance => eventInstance.StartDateTime >= DateTime.UtcNow && eventInstance.StartDateTime <= DateTime.UtcNow.AddMonths(1))
                .OrderBy(eventInstance => eventInstance.StartDateTime)
                .Include(eventInstance => eventInstance.EventBase)
                .ToListAsync();
            return Ok(eventInstances);
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var eventInstanceEntity = await _context.EventInstances.Include(ei => ei.EventBase.Place).FirstOrDefaultAsync(ei => ei.Id == id);

            if (eventInstanceEntity == null)
            {
                return NotFound();
            }
            return Ok(eventInstanceEntity);
        }

        [Route("{placeCode}")]
        [HttpGet]
        public async Task<IActionResult> Get(string placeCode)
        {
            var eventInstanceEntity = await _context.EventInstances.Include(ei => ei.EventBase.Place)
                .Where(ei => ei.EventBase.Place.Code == placeCode && ei.StartDateTime >= DateTime.UtcNow && ei.StartDateTime <= DateTime.UtcNow.AddDays(7))
                .ToListAsync();

            if (eventInstanceEntity == null)
            {
                return NotFound();
            }
            return Ok(eventInstanceEntity);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var eventInstanceEntity = await _context.EventInstances.FindAsync(id);

            if (eventInstanceEntity == null)
            {
                return NotFound();
            }

            _context.EventInstances.Remove(eventInstanceEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
