using System;
using Microsoft.EntityFrameworkCore;
using NotificationService.API.Persistence.Entities.DB.Interfaces;

namespace NotificationService.API.Persistence.Entities.DB;

public class NotificationDbContext : DbContext
{
    public DbSet<Template> Templates { get; set; }
    public DbSet<TemplateType> TemplateType { get; set; }

    public NotificationDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
