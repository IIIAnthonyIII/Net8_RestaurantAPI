using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.ApplicationTests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId ()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);
        
        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "testowner@gmail.com", [], null, null);
        userContextMock
            .Setup(uc => uc.GetCurrentUser())
            .Returns(currentUser);
        
        var mapperMock = new Mock<IMapper>();
        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapperMock
            .Setup(m => m.Map<Restaurant>(command))
            .Returns(restaurant);

        var commandHanler = new CreateRestaurantCommandHandler(
            loggerMock.Object, 
            restaurantsRepositoryMock.Object,
            userContextMock.Object, 
            mapperMock.Object);

        // Act
        var result = await commandHanler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantsRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once); // Verifica que se creó una vez
    }
}