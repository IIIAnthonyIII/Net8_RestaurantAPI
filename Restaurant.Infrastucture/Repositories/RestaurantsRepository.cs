using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastucture.Persistence;

namespace Restaurants.Infrastucture.Repositories;

internal class RestaurantsRepository (RestaurantsDBContext dBContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllMActhingAsync ()
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

    public async Task<IEnumerable<Restaurant>> GetAllMacthingAsync (string? searchPhrase)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var restaurants = await dBContext
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower) ||
                                                      r.Description.ToLower().Contains(searchPhraseLower)))
            .ToListAsync();
        return restaurants;
    }
}
