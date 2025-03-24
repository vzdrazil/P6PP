using BookingPayments.API.Data.Seeding;
using BookingPayments.API.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookingPayments.API.Data;
using BookingPayments.API.Entities.Seeding;

public class BookPayDbContext : DbContext
{
    public DbSet<Bookings> Bookings { get; set; }
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<RoomStatus> RoomStatuses { get; set; }
    public DbSet<Discounts> Discounts { get; set; }
    public DbSet<BookingStatus> BookingStatus { get; set; }
    public DbSet<Services> Services { get; set; }
    
    public BookPayDbContext(DbContextOptions<BookPayDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>().HasData(DiscountsInit.GetDiscounts());

        modelBuilder.Entity<Room>().HasData(RoomsInit.GetRooms());

        modelBuilder.Entity<Service>().HasData(ServicesInit.GetServices());

        modelBuilder.Entity<Booking>().HasData(BookingsInit.GetBookings());
        modelBuilder.Entity<Services>().HasData(servicesInit.GetServices());

        base.OnModelCreating(modelBuilder);
    }

}