using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle (UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Actualizando usuario: {userId} con {@Request}", currentUser!.Id, request);
        var dbUser = await userStore.FindByIdAsync(currentUser!.Id, cancellationToken)
            ?? throw new NotFoundException("Usuario", currentUser.Id);
        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
