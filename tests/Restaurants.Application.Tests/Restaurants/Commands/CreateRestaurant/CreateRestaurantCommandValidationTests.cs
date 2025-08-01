using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidationTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors ()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "TestNew",
            Category = "rapida",
            Description = "Test description",
            ContactEmail = "test@gmail.com",
            PostalCode = "12-345"
        };
        var validator = new CreateRestaurantCommandValidation();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors ()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "rap",
            ContactEmail = "@gmail.com",
            PostalCode = "12345"
        };
        var validator = new CreateRestaurantCommandValidation();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory]
    [InlineData("rapida")]
    [InlineData("casual")]
    [InlineData("mexicana")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty (string category)
    {
        // Arrange
        var validation = new CreateRestaurantCommandValidation();
        var command = new CreateRestaurantCommand { Category = category };

        // Act
        var result = validation.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory]
    [InlineData("10200")]
    [InlineData("102-20")]
    [InlineData("10 2000")]
    [InlineData("10-2 20")]
    public void Validator_ForValidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty (string postalCode)
    {
        // Arrange
        var validation = new CreateRestaurantCommandValidation();
        var command = new CreateRestaurantCommand { PostalCode = postalCode };

        // Act
        var result = validation.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}