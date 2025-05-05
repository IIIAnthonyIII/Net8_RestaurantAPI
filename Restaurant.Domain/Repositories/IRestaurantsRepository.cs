using Restaurants.Domain.Entities;
namespace Restaurants.Domain.Repositories;
public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync ();
    Task<Restaurant?> GetIDAsync (int id);
    Task<int> CreateAsync (Restaurant entity);
}
