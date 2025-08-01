using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastucture.Authorization.Requirements;
using Xunit;

namespace Restaurants.Infrastucture.Tests.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucced ()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateMultipleRestaurantsRequirementHandler>>();

        var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(uc => uc.GetCurrentUser())
            .Returns(currentUser);

        var restaurants = new List<Restaurant>
        {
            new ()
            { 
                OwnerId = currentUser.Id
            },
            new ()
            { 
                OwnerId = currentUser.Id
            },
            new ()
            {
                OwnerId = "2"
            },
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(restaurants);
        var requirement = new CreateMultipleRestaurantsRequirement(2);
        var handler = new CreateMultipleRestaurantsRequirementHandler(
            loggerMock.Object,
            restaurantsRepositoryMock.Object,
            userContextMock.Object
        );
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        await handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail ()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateMultipleRestaurantsRequirementHandler>>();

        var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(uc => uc.GetCurrentUser())
            .Returns(currentUser);

        var restaurants = new List<Restaurant>
        {
            new ()
            {
                OwnerId = currentUser.Id
            },
            new ()
            {
                OwnerId = "2"
            },
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(restaurants);
        var requirement = new CreateMultipleRestaurantsRequirement(2);
        var handler = new CreateMultipleRestaurantsRequirementHandler(
            loggerMock.Object,
            restaurantsRepositoryMock.Object,
            userContextMock.Object
        );
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        await handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}