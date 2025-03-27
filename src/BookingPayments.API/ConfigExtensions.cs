using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API;

internal static class ConfigExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IRoomAppService>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BookPayDbContext>();

        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
        }
    }
}
