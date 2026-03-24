using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelManagment.Models;

namespace HotelManagment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           builder.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();

            builder.Entity<Reservation>()
                .HasMany(r => r.Clients)
                .WithMany(c => c.Reservations)
                .UsingEntity(j => j.ToTable("ReservationClients"));

            builder.Entity<Room>().Property(r => r.PriceForAdult).HasPrecision(18, 2);
            builder.Entity<Room>().Property(r => r.PriceForChild).HasPrecision(18, 2);
            builder.Entity<Reservation>().Property(r => r.TotalAmount).HasPrecision(18, 2);
        }
    }
}