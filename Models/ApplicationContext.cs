using Microsoft.EntityFrameworkCore;

namespace SmartHouse.WEB.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Record> Records { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
