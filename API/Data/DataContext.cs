using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Norm>()
                .HasKey(x => x.ParamCode);

            builder.Entity<Province>()
                .HasMany(p => p.Districts)
                .WithOne(d => d.Province)
                .HasForeignKey(d => d.ProvienceId);
            

            builder.Entity<District>()
                .HasMany(d => d.Communes)
                .WithOne(c => c.District)
                .HasForeignKey(c => c.DistrictId);


            builder.Entity<Commune>()
                .HasMany(c => c.Cities)
                .WithOne(ct => ct.Commune)
                .HasForeignKey(ct => ct.CommuneId);

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
