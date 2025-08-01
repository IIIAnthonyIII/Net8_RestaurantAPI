using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

// El int es lo que se espera que devuelva del command
public class CreateRestaurantCommandHandler (ILogger<CreateRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle (CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{userEmail} {userId} is creating restaurant {@Restaurant}", 
            currentUser!.Email, currentUser.Id, request);
        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;
        int id = await restaurantsRepository.CreateAsync(restaurant);
        return id;
    }
}
