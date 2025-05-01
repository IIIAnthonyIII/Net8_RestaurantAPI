using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync ();
        Task<RestaurantDto?> GetRestaurantByIdAsync (int id);
    }
}