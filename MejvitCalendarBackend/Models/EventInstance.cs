namespace MejvitCalendarBackend.Models
{
    public class EventInstance
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? Name { get; set; }

        public EventBase? EventBase { get; set; }
    }
}
