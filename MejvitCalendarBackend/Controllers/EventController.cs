using MejvitCalendarBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MejvitCalendarBackend.Controllers
{
    [Route("api/eventsa")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post(EventBase eventBase)
        {

            return Ok(eventBase.Id);
            //try
            //{
            //    _context.Categories.Add(category);
            //    await _context.SaveChangesAsync();
            //    return Ok(category.Id);
            //}
            //catch (Exception e)
            //{
            //    return Conflict(e.Message);
            //}
        }
    }
}
