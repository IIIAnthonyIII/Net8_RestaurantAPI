using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery (int restaurantid) : IRequest<IEnumerable<DishDto>>
{
    public int Id { get; } = restaurantid;
}
