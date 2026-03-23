using AuthService.Data;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration
               .GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService,
    AuthService.Services.AuthService>();

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider
                      .GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

app.MapOpenApi();
app.MapScalarApiReference();
app.UseAuthorization();
app.MapControllers();
app.Run();