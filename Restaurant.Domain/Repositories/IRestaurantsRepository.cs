using Restaurants.Domain.Entities;
namespace Restaurants.Domain.Repositories;
public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllMActhingAsync ();
    Task<Restaurant?> GetByIdAsync (int id);
    Task<int> CreateAsync (Restaurant entity);
    Task DeleteAsync (Restaurant entity);
    Task SaveChanges ();
    Task<IEnumerable<Restaurant>> GetAllMacthingAsync (string? searchPhrase);
}
