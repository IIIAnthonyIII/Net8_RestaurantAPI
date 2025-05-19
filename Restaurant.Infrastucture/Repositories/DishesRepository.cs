using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastucture.Persistence;

namespace Restaurants.Infrastucture.Repositories;

internal class DishesRepository (RestaurantsDBContext dbContext) : IDishesRepository
{
    public async Task<int> CreateAsync (Dish entity)
    {
        dbContext.Dishes.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
