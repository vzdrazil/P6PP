using Microsoft.EntityFrameworkCore;
using NotificationService.API.Extensions;
using NotificationService.API.Features;
using NotificationService.API.Persistence.Entities.DB;
// This is just an example how you CAN structure your microservice,
// you can do it differently, but this is lightweight and easy to understand.

var builder = WebApplication.CreateBuilder(args);

// Configure port here + launchSettings.json ( + later Dockerfile EXPOSE XXXX)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5181);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Fix server version hardcode
ServerVersion serverVersion = new MariaDbServerVersion("10.5.26");

String connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<NotificationDbContext>(
    optionsBuilder => optionsBuilder.UseMySql(connectionString, serverVersion)
);

// Register services
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Create DB, tables, etc.
// Also, you can use your own ORM or database library, this is just an example where i use Dapper (ULTRA FAST)
// In group we have chosen to use MySQL server -> instance your own database there via docker compose in the future 
// (For development you can use your local database so debugging is easier)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}

if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// All endpoints here are defined in Features folder
// Here you can register your endpoints, for example:
app.UseEndpoints(endpoints =>
{
    SendEmailEndpoint.SendEmail(endpoints);
    SendRegistrationEmailEndpoint.SendRegistrationEmail(endpoints);
});

app.Run();
