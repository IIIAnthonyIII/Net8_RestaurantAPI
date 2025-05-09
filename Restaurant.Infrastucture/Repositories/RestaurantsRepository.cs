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
    public async Task<Restaurant?> GetByIdAsync (int id)
    {
        var restaurant = await dBContext.Restaurants
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }
    public async Task<int> CreateAsync (Restaurant entity)
    {
        dBContext.Restaurants.Add(entity);
        await dBContext.SaveChangesAsync();
        return entity.Id;
    }
    public async Task DeleteAsync (Restaurant entity)
    {
        dBContext.Remove(entity);
        await dBContext.SaveChangesAsync();
    }
    public async Task SaveChanges () => await dBContext.SaveChangesAsync();
}
