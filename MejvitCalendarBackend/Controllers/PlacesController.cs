using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MejvitCalendarBackend.Controllers
{
    [ApiController]
    public class PlacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PlacesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("api/places")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var places = await _context.Places.OrderBy(p => p.Name).ToListAsync();
            return Ok(places);
        }

        [Route("api/places/code={code}")]
        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return NotFound();
            }

            Place? place = await _context.Places.FirstOrDefaultAsync(p => p.Code == code);
            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }


        [Route("api/places")]
        [HttpPost]
        public async Task<IActionResult> Post(Place place)
        {
            try
            {
                _context.Places.Add(place);
                await _context.SaveChangesAsync();
                return Ok(place.Id);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Route("api/places/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
