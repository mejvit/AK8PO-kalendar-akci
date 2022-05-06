﻿namespace MejvitCalendarBackend.Models
{
    public class EventInstance : Entity
    {
        public EventCategory Category { get; set; }

        public EventBase EventBase { get; set; }
        public Place Place { get; set; }
    }
}