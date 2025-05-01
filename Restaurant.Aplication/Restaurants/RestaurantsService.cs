using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;
internal class RestaurantsService (IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync ()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = restaurants.Select(RestaurantDto.FromEntityRestaurant);
        return restaurantsDto!;
    }
    public async Task<RestaurantDto?> GetRestaurantByIdAsync (int id)
    {
        logger.LogInformation($"Get restaurant {id}");
        var restaurant = await restaurantsRepository.GetIDAsync(id);
        var restaurantsDto = RestaurantDto.FromEntityRestaurant(restaurant);
        return restaurantsDto;
    }
}
