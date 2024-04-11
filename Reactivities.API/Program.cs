using Microsoft.EntityFrameworkCore;
using Reactivities.API.Extensions;
using Reactivities.API.MiddleWare;
using Reactivities.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Application Service Extension
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.

// Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

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
