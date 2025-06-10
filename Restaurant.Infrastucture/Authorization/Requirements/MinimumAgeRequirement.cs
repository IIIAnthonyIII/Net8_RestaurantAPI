using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastucture.Authorization.Requirements;

public class MinimumAgeRequirement (int minimunAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimunAge;
}
