using NotificationService.API.Services;

namespace NotificationService.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register MailAppService
        services.AddSingleton<MailAppService>();

        // Register other services if needed
        // services.AddScoped<OtherService>();
    }
}
