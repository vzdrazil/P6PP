using BookingPayments.API.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookingPayments.API.Data;

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

}