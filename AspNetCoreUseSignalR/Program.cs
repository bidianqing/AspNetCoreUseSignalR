using AspNetCoreUseSignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/chathub");

app.Run();
