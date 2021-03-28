using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Commune> Communes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Station> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Commune>()
                .HasKey(x => new { x.CommuneName, x.DistrictName});
            builder.Entity<Commune>()
                .HasMany(x => x.Cities)
                .WithOne(x => x.Commune)
                .HasForeignKey(x => new { x.CommuneName, x.DistrictName });

            builder.Entity<City>()
                .HasKey(x => x.Id);
            builder.Entity<City>()
                .HasMany(x => x.Stations)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId);

            builder.Entity<Station>()
                .HasKey(x => x.Id);


        }
    }
}
