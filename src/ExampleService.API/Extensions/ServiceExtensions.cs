using ExampleService.API.Features;
using ExampleService.API.Persistence;
using ExampleService.API.Persistence.Repositories;

namespace ExampleService.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Db Context 
        services.AddSingleton<DapperContext>();
        services.AddSingleton<DatabaseInitializer>();
        services.AddScoped<ExampleRepository>();
        
        // Register Services
        services.AddScoped<Services.ServiceExample>();
        
        // Register Memory Cache
        services.AddMemoryCache();

        services.AddSingleton<CreateExampleRequestValidator>();
        services.AddScoped<CreateExampleHandler>();
        
        services.AddSingleton<GetExampleListRequestValidator>();
        services.AddScoped<GetExampleListHandler>();
    }
}