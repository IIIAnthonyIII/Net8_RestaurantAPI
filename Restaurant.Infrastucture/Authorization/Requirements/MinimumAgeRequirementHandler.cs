using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastucture.Authorization.Requirements;

public class MinimumAgeRequirementHandler (ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync (AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null || currentUser.DateOfBirth == null)
        {
            logger.LogWarning("El usuario no se ha encontrado");
            context.Fail();
            return Task.CompletedTask;
        }
        logger.LogInformation("Usuario: {Email}, Fecha de nacimiento: {DOB} - Handling MinimumAgeRequirement",
            currentUser.Email, currentUser.DateOfBirth);
        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogWarning("Authorization succeded");
            context.Succeed(requirement);
        } else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
