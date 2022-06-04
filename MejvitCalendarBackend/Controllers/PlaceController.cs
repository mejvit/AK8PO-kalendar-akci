using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
using MejvitCalendarBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MejvitCalendarBackend.Controllers
{
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PlaceController(ApplicationDbContext context)
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

            PlaceEntity? placeEntity = await _context.Places.FirstOrDefaultAsync(p => p.Code == code);
            Place? place = null;

            if (placeEntity != null)
            {
                place = new Place()
                {
                    Code = placeEntity.Code,
                    Name = placeEntity.Name,
                    Description = placeEntity.Description
                };
            }
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
                PlaceEntity placeEntity = new PlaceEntity()
                {
                    Code = place.Code,
                    Name = place.Name
                };
                _context.Places.Add(placeEntity);
                await _context.SaveChangesAsync();
                return Ok(place.Id);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Route("api/places/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Place updatedPlace)
        {
            if (updatedPlace == null || updatedPlace.Id != id)
            {
                return BadRequest();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            _context.Entry<PlaceEntity>(place).CurrentValues.SetValues(updatedPlace);
            await _context.SaveChangesAsync();

            return new NoContentResult();
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
