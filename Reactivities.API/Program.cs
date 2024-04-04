using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Activities;
using Reactivities.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Database Context
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR Service
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
// registers all services in the same assembly as List.Hander = Reactivities.Application.Activities namespace


// Client: CORS policy
builder.Services.AddCors(options =>
{
    string clientUrl = "http://localhost:3000";
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(clientUrl);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Client: CORS policy
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// Database creation & data seeding

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync(); // Applies any pending migrations. Creates the database if it does not exist
    await Seed.SeedDataAsync(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
