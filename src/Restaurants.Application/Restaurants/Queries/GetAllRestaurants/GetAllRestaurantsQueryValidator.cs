using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSizes = [5, 10, 20, 50, 100];
    private string[] allowSortBy = [nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];
    public GetAllRestaurantsQueryValidator ()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"PageSize solo debe ser [{string.Join(",", allowPageSizes)}]");
        RuleFor(r => r.SortBy)
            .Must(value => allowSortBy.Contains(value))
            .When(r => r.SortBy != null)
            .WithMessage($"Sort es opcional, pero solo debe ser [{string.Join(",", allowSortBy)}]");
    }
}
