using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
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
            var eventInstances = await _context.EventInstances.Include(eventInstance => eventInstance.EventBase).ToListAsync();
            return Ok(eventInstances);
            //var eventInstances = new List<EventInstance>()
            //{
            //    new EventInstance()
            //    {
            //        StartDateTime = new DateTime(2022,5,30,7,00,00),
            //        EndDateTime = new DateTime(2022,5,30,8,00,00).ToUniversalTime(),
            //        Name = "Brno — mše svatá z kostela sv. Tomáše"
            //    },
            //    new EventInstance()
            //    {
            //        StartDateTime = new DateTime(2022,5,30,14,00,00).ToUniversalTime(),
            //        EndDateTime = new DateTime(2022,5,30,15,00,00).ToUniversalTime(),
            //        Name = "Brno — mše svatá z kostela sv. Tomáše"
            //    },
            //    new EventInstance()
            //    {
            //        StartDateTime = new DateTime(2022,5,30,17,00,00).ToUniversalTime(),
            //        EndDateTime = new DateTime(2022,5,30,18,00,00).ToUniversalTime(),
            //        Name = "Brno — mše svatá z katedrály sv. Petra a Pavla"
            //    },
            //    new EventInstance()
            //    {
            //        StartDateTime = new DateTime(2022,5,30,18,00,00).ToUniversalTime(),
            //        EndDateTime = new DateTime(2022,5,30,19,00,00).ToUniversalTime(),
            //        Name = "Uhřínov — mše svatá z kostela Povýšení sv. Kříže"
            //    },
            //    new EventInstance()
            //    {
            //        StartDateTime = new DateTime(2022,5,30,18,00,00),
            //        EndDateTime = new DateTime(2022,5,30,19,00,00),
            //        Name = "Měřín — mše svatá z kostela sv. Jana Křtitele"
            //    }
            //};
            //return Ok(eventInstances);
        }
    }
}
