using Microsoft.EntityFrameworkCore;
using MoneyPlay.Api.Repositories;
using MoneyPlay.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddGrpc();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite("Data Source=mydatabase.db");
    
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

var app = builder.Build();

app.MapGrpcService<MoneyGoalsService>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    db.Database.EnsureCreated();
}

app.Run();