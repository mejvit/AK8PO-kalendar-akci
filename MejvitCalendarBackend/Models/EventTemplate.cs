using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models
{
    public class EventTemplate : Entity
    {
        [Required]
        public string Name { get; set; }

        public string? RRule { get; set; }
    }
}
