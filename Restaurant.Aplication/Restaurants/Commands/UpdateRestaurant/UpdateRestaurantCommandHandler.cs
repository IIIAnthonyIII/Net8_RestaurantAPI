using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler (ILogger<CreateRestaurantCommandHandler> logger,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle (UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant: {RestaurantId} con {@UpdateRestaurant}", request.Id, request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id) 
            ?? throw new NotFoundException("Restaurante", request.Id.ToString());
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();
        mapper.Map(request, restaurant);
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;
        await restaurantsRepository.SaveChanges();
    }
}
