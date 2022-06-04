namespace MejvitCalendarBackend.Models
{
    public class Place
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public string? Description { get; set; }

        public string? HomePageUrl { get; set; }

        public string? DefaultStreamUrl { get; set; }

        public double? GpsLattitude { get; set; }

        public double? GpsLongitude { get; set; }
    }
}
