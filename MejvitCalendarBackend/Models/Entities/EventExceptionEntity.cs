using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models.Entities
{
    [Table("EventException")]
    public class EventExceptionEntity : Entity
    {
        [ForeignKey("EventBase")]
        public int EventBaseId { get; set; }
        public EventBaseEntity EventBase { get; set; }
        public DateTime Date { get; set; }
    }
}
