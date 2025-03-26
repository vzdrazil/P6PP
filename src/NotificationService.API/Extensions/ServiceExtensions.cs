using NotificationService.API.Services;
using NotificationService.API.Features;

namespace NotificationService.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register MailAppService
        services.AddSingleton<MailAppService>();

        // Register SendEmail Services
        services.AddSingleton<SendEmailRequestValidator>();
        services.AddScoped<SendEmailHandler>();

        // Register TemplateAppService
        services.AddScoped<TemplateAppService>();

        // Register RegisterEmail Services
        services.AddSingleton<SendRegistrationEmailValidator>();
        services.AddScoped<SendRegistrationEmailHandler>();
    }
}

