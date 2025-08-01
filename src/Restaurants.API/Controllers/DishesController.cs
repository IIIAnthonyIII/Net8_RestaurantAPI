using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Infrastucture.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
[Authorize]
public class DishesController (IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish ([FromRoute] int restaurantId, CreateDishCommand createDishCommand)
    {
        createDishCommand.RestaurantId = restaurantId;
        var dishId = await mediator.Send(createDishCommand);
        return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AtLeast20)] //Solo si tiene al menos 20 años accede
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant ([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet ("{dishId}")]
    public async Task<ActionResult<DishDto>> GetByIdForRestaurant ([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }
    [HttpDelete("{dishId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDishesForRestaurant ([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }
}