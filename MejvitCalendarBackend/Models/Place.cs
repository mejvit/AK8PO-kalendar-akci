using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models
{
    [Table("Place")]
    public class Place : Entity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        [Url]
        public string? HomePageUrl { get; set; }

        [Url]
        public string? DefaultStreamUrl { get; set; }

        public double? GpsLattitude { get; set; }

        public double? GpsLongitude { get; set; }
    }
}
