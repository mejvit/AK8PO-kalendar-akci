using MejvitCalendarBackend.Data;
using MejvitCalendarBackend.Models;
using MejvitCalendarBackend.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace MejvitCalendarBackend.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventBaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventBaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventBase = await _context.Events.OrderBy(ev => ev.StartTime).ThenBy(ev => ev.Name).ToListAsync();
            return Ok(eventBase);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post(EventBase eventBase)
        {
            
            if (eventBase.PlaceId == null || eventBase.CategoryId == null)
                return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                EventBaseEntity eventBaseEntity = new EventBaseEntity()
                {
                    Name = eventBase.Name,
                    StartTime = eventBase.StartTime,
                    EndTime = eventBase.EndTime,
                    RRule = eventBase.RRule,
                    PlaceId = eventBase.PlaceId.Value,
                    CategoryId = eventBase.CategoryId.Value

                };
                _context.Events.Add(eventBaseEntity);
                await _context.SaveChangesAsync();


                if (eventBase.RRule != null)
                {
                    var vEvent = new CalendarEvent
                    {
                        Start = new CalDateTime(eventBaseEntity.StartTime.Value),
                        End = new CalDateTime(eventBaseEntity.EndTime.Value),
                        RecurrenceRules = new List<RecurrencePattern> { new RecurrencePattern(eventBaseEntity.RRule) },
                    };



                    var calendar = new Calendar();
                    calendar.Events.Add(vEvent);
                    var searchStart = eventBaseEntity.StartTime.Value;
                    var searchEnd = eventBaseEntity.StartTime.Value.AddYears(1);
                    var occurrences = calendar.GetOccurrences(searchStart, searchEnd);

                    foreach (Occurrence singleEvent in occurrences)
                    {
                        EventInstanceEntity eventInstance = new EventInstanceEntity()
                        {
                            EventBaseId = eventBaseEntity.Id,
                            StartDateTime = singleEvent.Period.StartTime.AsUtc,
                            EndDateTime = singleEvent.Period.EndTime.AsUtc

                        };
                        _context.EventInstances.Add(eventInstance);
                    }
                }
                else
                {
                    EventInstanceEntity eventInstance = new EventInstanceEntity()
                    {
                        EventBaseId = eventBaseEntity.Id,
                        StartDateTime = eventBaseEntity.StartTime,
                        EndDateTime = eventBaseEntity.EndTime,
                    };
                    _context.EventInstances.Add(eventInstance);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(eventBaseEntity.Id);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return Conflict(e.Message);
            }
        }
    }
}
