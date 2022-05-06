using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models
{
    public class EventBase : Entity
    {
        [Required]
        public string Name { get; set; }

        public string? RRule { get; set; }

        public Place Place { get; set; }

        public EventCategory Category { get; set; }

        public List<EventException>? EventExceptions { get; set; }
    }
}
