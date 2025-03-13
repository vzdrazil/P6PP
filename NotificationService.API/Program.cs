using NotificationService.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MailAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Testovací endpoint pro odeslání e-mailu
app.MapGet("/test-email", async (MailAppService emailService) =>
{
    IList<string> recipients = new List<string> { "recipient@example.com" };
    await emailService.SendEmailAsync(recipients, "Test Email", "This is a test email.");
    return Results.Ok("Email sent successfully");
});

app.Run();
