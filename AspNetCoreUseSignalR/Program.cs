using AspNetCoreUseSignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = new TimeSpan(0, 0, 10);
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/chathub");

app.Run();
