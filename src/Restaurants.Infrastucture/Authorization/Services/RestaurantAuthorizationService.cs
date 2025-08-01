using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastucture.Authorization.Services;

public class RestaurantAuthorizationService (ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize (Restaurant restaurant, ResourceOperation operation)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Authorize user {UserEmail} to {Operation} for restaurant {RestaurantName}",
            currentUser!.Email, operation, restaurant.Name);
        //Solo se crea y lee
        if (operation == ResourceOperation.Create || operation == ResourceOperation.Read)
        {
            logger.LogInformation("Operación para leer/crear - Autorización exitosa");
            return true;
        }
        //Solo elimina si es Admin
        if (operation == ResourceOperation.Delete && currentUser!.IsinRole(UserRoles.Admin))
        {
            logger.LogInformation("Usuario Admin - Operación eliminar - Autorización exitosa");
            return true;
        }
        //Solo actualiza o elimina si es el propietario (owner) del restaurante
        if ((operation == ResourceOperation.Update || operation == ResourceOperation.Delete)
            && currentUser!.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - Autorización exitosa");
            return true;
        }
        return false;
    }
}
