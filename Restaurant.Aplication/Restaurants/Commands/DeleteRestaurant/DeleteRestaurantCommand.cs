using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand (int Id) : IRequest
{
    public int Id { get; } = Id;
}
