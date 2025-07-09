using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurants.ApplicationTests.Users;

public class UserContextTests
{
    // UserContext devuelve correctamente la información del usuario autenticado cuando hay un usuario presente en el contexto.
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser ()
    {
        // Arrange
        var dateIfBirth = new DateOnly(1990, 1, 1);
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@gmail.com"),
            new(ClaimTypes.Role, UserRoles.Admin),
            new(ClaimTypes.Role, UserRoles.User),
            new("Nationality", "Italiano"),
            new("DateOfBirth", dateIfBirth.ToString("yyyy-MM-dd"))
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });
        var userContext = new UserContext(httpContextAccessorMock.Object);

        // Act
        var currentUser = userContext.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@gmail.com");
        currentUser.Roles.Should().Contain(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("Italiano");
        currentUser.DateOfBirth.Should().Be(dateIfBirth);
    }

    // UserContext devuelve una excepción cuando no hay un usuario presente en el contexto.
    [Fact()]
    public void GetCurrentUser_WithUserContextNotPresent_ShouldThrowInvalidOperationException ()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);
        var userContext = new UserContext(httpContextAccessorMock.Object);

        // Act
        Action action = () => userContext.GetCurrentUser();

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("User context no está presente");
    }
}