using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models.Entities
{
    [Table("Place")]
    public class PlaceEntity : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Code { get; set; }

        public string? Description { get; set; }

        [Url]
        public string? HomePageUrl { get; set; }

        [Url]
        public string? DefaultStreamUrl { get; set; }

        public double? GpsLattitude { get; set; }

        public double? GpsLongitude { get; set; }
    }
}
