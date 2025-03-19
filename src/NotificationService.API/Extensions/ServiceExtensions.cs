using NotificationService.API.Services;
using NotificationService.API.Features;

namespace NotificationService.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register MailAppService
        services.AddSingleton<MailAppService>();

        // Register Validators
        services.AddSingleton<SendEmailRequestValidator>();

        // Register Handlers
        services.AddScoped<SendEmailHandler>();

        // Register other services if needed
        // services.AddScoped<OtherService>();
    }
}

