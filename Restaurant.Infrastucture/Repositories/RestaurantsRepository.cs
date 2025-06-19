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

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMacthingAsync (string? searchPhrase, int pageSize, int pageNumber)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dBContext.Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower) ||
                                                      r.Description.ToLower().Contains(searchPhraseLower)));
        var totalCount = await baseQuery.CountAsync(); // Count total items for pagination
        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        //Formula para paginacion
        //pageSize = 5, pageNumber = 3,
        //Skip = pageSize * (pageNumber - 1)
        //Skip = 5 * (3 - 1) = 10
        //Salta 10 registros y toma los 5 siguientes
        return (restaurants, totalCount);
    }
}
