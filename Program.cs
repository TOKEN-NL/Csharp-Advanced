using Csharp_Advanced.Models;
using Csharp_Advanced.Repositories;
using Csharp_Advanced.Seeding;
using Csharp_Advanced.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    // HTTP-configuratie
    options.ListenAnyIP(80); // Luister naar HTTP op poort 80

    // HTTPS-configuratie
    options.ListenAnyIP(7279, listenOptions =>
    {
        listenOptions.UseHttps(); // Gebruik HTTPS
    });
});

// Register services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<LocationRepository>();
builder.Services.AddScoped<LocationService>();

builder.Services.AddScoped<ReservationRepository>();
builder.Services.AddScoped<ReservationService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Seed de database
    DatabaseSeeder.Seed(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin());
}

//app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();
