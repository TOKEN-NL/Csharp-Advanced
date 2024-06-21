using Csharp_Advanced.Models;
using Csharp_Advanced.Repositories;
using Csharp_Advanced.Seeding;
using Csharp_Advanced.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

// C# advanced opdracht Kento Bergsma S1105076

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

builder.Services.AddScoped<CustomerRepository>();

// Add services to the container.

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2", Version = "v2" });

    // Voeg API-versie-aanduiding toe aan Swagger-documenten
    c.DocInclusionPredicate((version, desc) =>
    {
        if (!desc.TryGetMethodInfo(out var methodInfo)) return false;

        var versions = methodInfo.DeclaringType.GetCustomAttributes(true)
            .OfType<ApiVersionAttribute>()
            .SelectMany(attr => attr.Versions);

        return versions.Any(v => $"v{v}" == version);
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // Voeg XML-documentatie toe aan Swagger als het bestand bestaat
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

//seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Wipe DB      -- standaard uit, alleen aan voor opnieuw seeden
    // DatabaseSeeder.DeleteAllEntries(dbContext);

    // Seed de database
    DatabaseSeeder.Seed(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");

    }); app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin());
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
