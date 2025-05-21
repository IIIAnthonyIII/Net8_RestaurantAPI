using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishesForRestaurantCommandHandler (ILogger<DeleteDishesForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle (DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Eliminado platos para el restaurante con id {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException("Restaurante", request.RestaurantId.ToString());
        await dishesRepository.DeleteAsync(restaurant.Dishes);
    }
}
