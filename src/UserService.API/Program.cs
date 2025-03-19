using UserService.API.Extensions;
using UserService.API.Features;
using UserService.API.Features.Roles;
using UserService.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5189);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var databaseInitializer = services.GetRequiredService<DatabaseInitializer>();

    await databaseInitializer.InitializeDatabaseAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // USER ENDPOINTS
    GetUserByIdEndpoint.Register(endpoints);
    DeleteUserEndpoint.Register(endpoints);
    UpdateUserEndpoint.Register(endpoints);
    CreateUserEndpoint.Register(endpoints);
    
    /*
    // ROLE ENDPOINTS
    GetRoleByIdEndpoint.Register(endpoints);
    CreateRoleEndpoint.Register(endpoints);
    */
});

app.Run();

// TODO: LAST LOGGED IN (JAK TO VYŘEŠIT??)
// TODO: PAGINATION, AUTH DELETE

