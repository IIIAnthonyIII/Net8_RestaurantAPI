using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastucture.Persistence;

namespace Restaurants.Infrastucture.Repositories;

internal class RestaurantsRepository (RestaurantsDBContext dBContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync ()
    {
        var restaurants = await dBContext.Restaurants.ToListAsync();
        return restaurants;
    }
    public async Task<Restaurant?> GetIDAsync (int id)
    {
        var restaurant = await dBContext.Restaurants
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }
}
