using AuthService.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser, Role, string>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<TokenBlackList> TokenBlackLists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TokenBlackList>()
            .HasOne(t => t.User)
            .WithMany(u => u.TokenBlackLists)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

        builder.Entity<TokenBlackList>()
            .HasIndex(t => t.Token)
            .IsUnique();

        builder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();
    }
}