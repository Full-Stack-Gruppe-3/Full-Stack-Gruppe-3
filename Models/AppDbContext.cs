using Microsoft.EntityFrameworkCore;

namespace Full_Stack_Gruppe_3.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Observation> Observations { get; set; }
        public DbSet<RootObject> RootObjects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships
            modelBuilder.Entity<RootObject>()
                .HasMany(r => r.Observations)
                .WithOne(o => o.RootObject)
                .HasForeignKey(o => o.TimeSeriesId);



            // Ignore Level as it's embedded in Observation
            modelBuilder.Entity<Observation>()
                .OwnsOne(o => o.Level);
        }
    }
}
