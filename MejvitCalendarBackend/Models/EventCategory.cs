using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models
{
    [Table("EventCategory")]
    public class EventCategory : Entity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
