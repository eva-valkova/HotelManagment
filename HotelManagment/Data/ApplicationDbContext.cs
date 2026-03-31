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

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservation>()
                .HasMany(r => r.Clients)
                .WithMany(c => c.Reservations);

            builder.Entity<Room>(entity =>
            {
                entity.Property(e => e.PriceForAdult).HasPrecision(18, 2);
                entity.Property(e => e.PriceForChild).HasPrecision(18, 2);
            });

            builder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);

                entity.HasOne(r => r.ReservedRoom)
                      .WithMany()
                      .HasForeignKey(r => r.RoomId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
