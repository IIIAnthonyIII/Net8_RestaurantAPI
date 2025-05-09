using Restaurants.Application.Extensions;
using Restaurants.Infrastucture.Extensions;
using Restaurants.Infrastucture.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//Los Seeders van despu�s de que se construya la app
var scope = app.Services.CreateScope();
var seeders = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeders.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
