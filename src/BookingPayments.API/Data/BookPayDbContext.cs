using BookingPayments.API.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookingPayments.API.Data;
using BookingPayments.API.Entities.Seeding;

public class BookPayDbContext : DbContext
{
    public DbSet<Bookings> Bookings { get; set; }
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<Discounts> Discounts { get; set; }
    public DbSet<BookingStatus> BookingStatus { get; set; }
    public DbSet<Services> Services { get; set; }
    
    public BookPayDbContext(DbContextOptions<BookPayDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var bookingStatusInit = new BookingStatusInit();
        modelBuilder.Entity<BookingStatus>().HasData(bookingStatusInit.GetBookingStatuses());

        var discountsInit = new DiscountsInit();
        modelBuilder.Entity<Discounts>().HasData(discountsInit.GetDiscounts());

        var roomsInit = new RoomsInit();
        modelBuilder.Entity<Rooms>().HasData(roomsInit.GetRooms());

        var servicesInit = new ServicesInit();
        modelBuilder.Entity<Services>().HasData(servicesInit.GetServices());

        var bookingsInit = new BookingsInit();
        modelBuilder.Entity<Bookings>().HasData(bookingsInit.GetBookings());
    }

}