using MejvitCalendarBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MejvitCalendarBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PlaceEntity> Places { get; set; }
        public DbSet<EventBaseEntity> Events { get; set; }
        public DbSet<EventCategoryEntity> Categories { get; set; }
        public DbSet<EventInstanceEntity> EventInstances { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }


}
