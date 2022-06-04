using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models.Entities
{
    [Table("EventInstance")]
    public class EventInstanceEntity : Entity
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        [ForeignKey("EventBase")]
        public int? EventBaseId { get; set; }
        public EventBaseEntity EventBase { get; set; }
    }
}
