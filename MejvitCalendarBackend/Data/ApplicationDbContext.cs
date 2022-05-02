using MejvitCalendarBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MejvitCalendarBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Place> Places { get; set; }
        public DbSet<EventCategory> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }


}
