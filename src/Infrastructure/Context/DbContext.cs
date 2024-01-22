using Domain.Models.BookingModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingCancelled> BookingCancelleds { get; set; }
    public DbSet<BookingUserStatus> BookingUserStatus { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.UserId);
        modelBuilder.Entity<Booking>().HasKey(x => x.BookingId);
        modelBuilder.Entity<BookingCancelled>().HasKey(x => x.BookingCancelledId);
        modelBuilder.Entity<BookingUserStatus>().HasKey(x => x.BookingUserStatusId);

        modelBuilder.Entity<Booking>().HasMany(x => x.BookingUserStatus).WithOne(x => x.Booking);
        modelBuilder.Entity<User>().HasMany(x=>x.BookingsStatus).WithOne(x=>x.User);

        modelBuilder.Entity<BookingCancelled>().HasOne(x => x.BookingUserStatus).WithMany(x => x.BookingCancelleds);
    }
}