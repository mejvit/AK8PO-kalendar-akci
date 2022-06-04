namespace MejvitCalendarBackend.Models
{
    public class EventBase
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public string? RRule { get; set; }

        public int? PlaceId { get; set; }
        public Place? Place { get; set; }

        public int? CategoryId { get; set; }
        public EventCategory? Category { get; set; }

        public List<EventException>? EventExceptions { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
