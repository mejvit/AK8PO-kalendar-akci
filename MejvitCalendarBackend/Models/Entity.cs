using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MejvitCalendarBackend.Models
{
    public abstract class Entity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
