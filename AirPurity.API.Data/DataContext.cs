using AirPurity.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirPurity.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Norm> Norms { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationSubject> NotificationSubjects { get; set; }

    }
}
