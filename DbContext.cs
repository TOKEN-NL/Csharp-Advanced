using Microsoft.EntityFrameworkCore;

namespace Csharp_Advanced
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .IsRequired(); 
             
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Reservations)
                .IsRequired(); 

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Images);             
               

        }
    }
}
