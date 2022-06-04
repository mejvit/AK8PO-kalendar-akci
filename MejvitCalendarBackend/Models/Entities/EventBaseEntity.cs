using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models.Entities
{
    [Table("EventBase")]
    public class EventBaseEntity : Entity
    {
        [Required]
        public string Name { get; set; }

        public string? RRule { get; set; }

        [ForeignKey("Place")]
        public int PlaceId { get; set; }

        [ForeignKey("EventCategory")]
        public int CategoryId { get; set; }

        public PlaceEntity Place { get; set; }

        public EventCategoryEntity Category { get; set; }

        public List<EventExceptionEntity>? EventExceptions { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
