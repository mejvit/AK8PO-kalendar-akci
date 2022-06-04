namespace MejvitCalendarBackend.Models
{
    public class EventException
    {
        public int? EventId { get; set; }
        public EventBase? Event { get; set; }
        public DateTime? Date { get; set; }
    }
}
