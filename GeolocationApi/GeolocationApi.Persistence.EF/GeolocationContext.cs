using ColocationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeolocationApi.Persistence.EF
{
    public class GeolocationContext : DbContext
    {
        public DbSet<Geolocation> Geolocations { get; set; }

        public GeolocationContext(DbContextOptions<GeolocationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geolocation>()
                .HasKey(e => e.Ip)
                .HasName("PrimaryKey_GeolocationId");

            modelBuilder.Entity<Geolocation>()
                .HasIndex(e => e.Url)
                .IsUnique();
        }
    }
}
