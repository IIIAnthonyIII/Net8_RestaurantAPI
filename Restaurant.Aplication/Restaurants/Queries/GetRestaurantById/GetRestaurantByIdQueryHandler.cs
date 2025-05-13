using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler (ILogger<GetRestaurantByIdQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle (GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get restaurant: {RestaurantId}", request.Id);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        //var restaurantsDto = RestaurantDto.FromEntityRestaurant(restaurant);
        var restaurantsDto = mapper.Map<RestaurantDto>(restaurant);
        return restaurantsDto;
    }
}
