using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync ();
    Task<Restaurant?> GetByIdAsync (int id);
    Task<int> CreateAsync (Restaurant entity);
    Task DeleteAsync (Restaurant entity);
    Task SaveChanges ();
    Task<(IEnumerable<Restaurant>, int)> GetAllMacthingAsync (string? searchPhrase,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}
