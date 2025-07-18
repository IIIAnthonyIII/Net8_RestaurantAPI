using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Restaurants.API.Tests.Controllers;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    // Para crear una instancia de la aplicación web para pruebas
    private readonly WebApplicationFactory<Program> _factory;
    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact()]
    public async Task GetAll_ForValidRequest_Returns200Ok ()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var results = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

        // Assert
        results.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact()]
    public async Task GetAll_ForInvalidRequest_Returns400BadRequest ()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var results = await client.GetAsync("/api/restaurants");

        // Assert
        results.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}