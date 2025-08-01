using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;
    private readonly UpdateRestaurantCommandHandler _commandUpdateHandler;

    public UpdateRestaurantCommandHandlerTests ()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        _mapperMock = new Mock<IMapper>();
        _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();
        _commandUpdateHandler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restaurantAuthorizationServiceMock.Object,
            _restaurantsRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurant ()
    {
        // Arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = true
        };
        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Original Restaurant",
            Description = "Original Description",
            HasDelivery = false,
            OwnerId = "owner-id"
        };

        _restaurantsRepositoryMock
            .Setup(repo => repo.GetByIdAsync(restaurantId))
            .ReturnsAsync(restaurant);
        _restaurantAuthorizationServiceMock
            .Setup(a => a.Authorize(restaurant, ResourceOperation.Update))
            .Returns(true);

        // Act
        await _commandUpdateHandler.Handle(command, CancellationToken.None);

        // Assert
        _restaurantsRepositoryMock.Verify(r => r.SaveChanges(), Times.Once); // Verificar que guardó una vez
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once); // Verificar que se mapeó una vez
    }

    [Fact()]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException ()
    {
        // Arrange
        var restaurantId = 2;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        _restaurantsRepositoryMock
            .Setup(repo => repo.GetByIdAsync(restaurantId))
            .ReturnsAsync((Restaurant?)null);

        // Act
        Func<Task> act = async () => await _commandUpdateHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurante con id {restaurantId} no existe");
    }

    [Fact()]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowUnauthorizedException ()
    {
        // Arrange
        var restaurantId = 3;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };
        var existRestaurant = new Restaurant()
        {
            Id = restaurantId
        };
        _restaurantsRepositoryMock
            .Setup(repo => repo.GetByIdAsync(restaurantId))
            .ReturnsAsync(existRestaurant);
        _restaurantAuthorizationServiceMock
            .Setup(a => a.Authorize(existRestaurant, ResourceOperation.Update))
            .Returns(false);

        // Act
        Func<Task> act = async () => await _commandUpdateHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}