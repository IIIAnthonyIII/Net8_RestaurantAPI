using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidation : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidation ()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);
    }
}
