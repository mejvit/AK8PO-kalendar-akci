namespace MejvitCalendarBackend.Models
{
    public class EventInstance : Entity
    {
        public EventCategory Category { get; set; }

        public EventTemplate EventTemplate { get; set; }
        public Place Place { get; set; }
    }
}
