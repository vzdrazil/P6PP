using UserService.API.Features;
using UserService.API.Persistence;
using UserService.API.Persistence.Repositories;
using UserService.API.Services;

namespace UserService.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Db Context 
        services.AddSingleton<DapperContext>();
        services.AddSingleton<DatabaseInitializer>();
        
        // Register Repositories
        services.AddScoped<RoleRepository>();
        services.AddScoped<UserRepository>();
        
        // Register Services
        services.AddScoped<RoleService>();
        services.AddScoped<Services.UserService>();
        
        // Register Endpoints injections
        services.AddScoped<GetUserByIdHandler>();
        services.AddSingleton<GetUserByIdRequestValidator>();
        
        // Register Memory Cache
        services.AddMemoryCache();
    }
}