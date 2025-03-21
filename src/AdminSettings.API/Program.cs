using AdminSettings.Data;
using AdminSettings.Persistence;
using AdminSettings.Persistence.Repositories;
using AdminSettings.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AdminSettings API",
        Version = "v1"
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AdminSettingsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)))); 

builder.Services.AddHttpClient("UserApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5189/");
});


builder.Services.AddHttpClient<UserService>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<AuditLogService>();
builder.Services.AddScoped<AuditLogRepository>();
builder.Services.AddScoped<UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdminSettings API v1");
    });
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

