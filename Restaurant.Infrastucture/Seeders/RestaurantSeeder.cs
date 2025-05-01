using Restaurants.Domain.Entities;
using Restaurants.Infrastucture.Persistence;

namespace Restaurants.Infrastucture.Seeders;
internal class RestaurantSeeder (RestaurantsDBContext dBContext) : IRestaurantSeeder
{
    public async Task Seed ()
    {
        if (await dBContext.Database.CanConnectAsync())
        {
            if (!dBContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dBContext.Restaurants.AddRange(restaurants);
                await dBContext.SaveChangesAsync();
            }
        }
    }
    private IEnumerable<Restaurant> GetRestaurants ()
    {
        List<Restaurant> restaurants = [
            new ()
            {
                Name = "Restaurant A",
                Category = "Italian",
                Description = "A cozy Italian restaurant with a warm atmosphere.",
                ContactEmail = "contact@gmail.com",
                HasDelivery = true,
                Address = new ()
                {
                    City = "City A",
                    Street = "123 Main St",
                    PostalCode = "12345",
                },
                Dishes =
                [
                    new ()
                    {
                        Name = "Dish A1",
                        Description = "Description A1",
                        Price = 10.99m,
                    },
                    new ()
                    {
                        Name = "Dish A2",
                        Description = "Description A2",
                        Price = 12.99m,
                    }
                ]
            },
            new ()
            {
                Name = "Restaurant B",
                Category = "Ecuadoriam",
                Description = "The best restaurant",
                ContactEmail = "contactEc@gmail.com",
                HasDelivery = true,
                Address = new ()
                {
                    City = "Gye",
                    Street = "10 de Marzo",
                    PostalCode = "36548",
                },
            }
        ];
        return restaurants;
    }
}
