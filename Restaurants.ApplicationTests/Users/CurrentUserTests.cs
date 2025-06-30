using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.ApplicationTests.Users;

public class CurrentUserTests
{
    // TestMethod_Scenario_ExpextResult
    // Test donde se compara el dato de admin con el que está en roles.
    [Fact()]
    public void IsinRole_WithMatchingRole_ShouldReturnTrue ()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isRole = currentUser.IsinRole(UserRoles.Admin);

        // Assert
        isRole.Should().BeTrue();
    }

    // Test donde se compara el dato de admin con el owner propietario al no se iguales debería ser false.
    [Fact()]
    public void IsinRole_WithNoMatchingRole_ShouldReturnFalse ()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isRole = currentUser.IsinRole(UserRoles.Owner);

        // Assert
        isRole.Should().BeFalse();
    }

    // Test donde se compara el dato de Admin con admin en minusculas, al no ser iguales debería ser false.
    [Fact()]
    public void IsinRole_WithNoMatchingRoleCase_ShouldReturnFalse ()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isRole = currentUser.IsinRole(UserRoles.Admin.ToLower());

        // Assert
        isRole.Should().BeFalse();
    }
}