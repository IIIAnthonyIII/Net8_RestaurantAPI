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
        if (currentUser == null)
        {
            logger.LogWarning("El usuario no se ha autenticado");
            context.Fail();
            return Task.CompletedTask;
        }
        logger.LogInformation("Usuario: {Email}, Fecha de nacimiento: {DOB} - Handling MinimumAgeRequirement",
            currentUser.Email, currentUser.DateOfBirth);
        var today = DateOnly.FromDateTime(DateTime.Today);
        var minAgeDate = currentUser.DateOfBirth!.Value.AddYears(requirement.MinimumAge);
        if (minAgeDate <= today)
        {
            logger.LogWarning("Authorization succeded");
            context.Succeed(requirement);
        } else
            context.Fail();
        return Task.CompletedTask;
    }
}
