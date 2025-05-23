﻿using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler (ILogger<CreateDishCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle (CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creando nuevo plato: {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException("Plato", request.RestaurantId.ToString());
        var dish = mapper.Map<Dish>(request);
        return await dishesRepository.CreateAsync(dish);
    }
}
