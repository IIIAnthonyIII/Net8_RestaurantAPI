using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler (ILogger<CreateDishCommandHandler> logger,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle (CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creando nuevo plato: {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException("Plato", request.RestaurantId.ToString());
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();
        var dish = mapper.Map<Dish>(request);
        return await dishesRepository.CreateAsync(dish);
    }
}
