using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastucture.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandler (ILogger<CreateMultipleRestaurantsRequirementHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync (AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null) context.Fail();
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsCount = restaurants.Count(r => r.OwnerId == currentUser!.Id);
        if (restaurantsCount >= requirement.MinimumRestaurantCreate)
            context.Succeed(requirement);
        else
        {
            logger.LogInformation("El propietario {Email} no tiene suficientes restaurantes creados: {Count} < {Minimum}",
                currentUser!.Email, restaurantsCount, requirement.MinimumRestaurantCreate);
            context.Fail();
        }
    }
}
