using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetByIdForRestaurant;

public class GetByIdForRestaurantQueryHandler (ILogger<GetByIdForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper): IRequestHandler<GetByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recibiendo el plato con id {dishId} del restaurant con id {restaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException("Platos", request.RestaurantId.ToString());
        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId)
            ?? throw new NotFoundException("Plato", request.DishId.ToString());
        var result = mapper.Map<DishDto>(dish);
        return result;
    }
}