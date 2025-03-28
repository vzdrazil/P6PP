using BookingPayments.API.Data.Seeding;
using BookingPayments.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Data;

public sealed class BookPayDbContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Service> Services { get; set; }

    public BookPayDbContext(DbContextOptions<BookPayDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>().HasData(DiscountsInit.GetDiscounts());

        modelBuilder.Entity<Room>().HasData(RoomsInit.GetRooms());

        modelBuilder.Entity<Service>().HasData(ServicesInit.GetServices());

        modelBuilder.Entity<Booking>().HasData(BookingsInit.GetBookings());

        base.OnModelCreating(modelBuilder);
    }
}