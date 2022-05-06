using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models
{
    [Table("EventException")]
    public class EventException : Entity
    {
        public EventBase Event { get; set; }
        public DateTime Date { get; set; }
    }
}
