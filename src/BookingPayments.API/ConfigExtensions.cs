using BookingPayments.API.Application.Abstraction;

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
}
