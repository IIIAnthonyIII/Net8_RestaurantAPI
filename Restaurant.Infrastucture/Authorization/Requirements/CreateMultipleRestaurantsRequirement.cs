using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastucture.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirement (int minimumRestaurantCreated) : IAuthorizationRequirement
{
    public int MinimumRestaurantCreate { get; } = minimumRestaurantCreated;
}
