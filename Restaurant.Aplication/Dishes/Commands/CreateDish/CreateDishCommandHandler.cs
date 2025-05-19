using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler (ILogger<CreateDishCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle (CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creando nuevo plato: {@DishRequest}", request);
        var restaurant = restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException("Plato", request.RestaurantId.ToString());
        var dish = mapper.Map<Dish>(request);
        await dishesRepository.CreateAsync(dish);
    }
}
