using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
namespace Restaurants.Application.Restaurants.Validations;
public class CreateRestaurantDtoValidation : AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> _validateCategories = ["rapida", "casual", "mexicana"];
    public CreateRestaurantDtoValidation ()
    {
        RuleFor(dto => dto.Name)
            .Length(6, 100);
        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Descripción no puede estar vacio");
        RuleFor(dto => dto.Category)
            .NotEmpty()
            .WithMessage("Categoría no puede estar vacio");
        RuleFor(dto => dto.Category)
            .Must(_validateCategories.Contains)
            .WithMessage("Ingrese una categoría válida");
        RuleFor(dto => dto.ContactEmail)
            .EmailAddress() //Solo valida el @
            .WithMessage("Email no es valido");
        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Código postal no es valido (XX-XXX)");
    }
}
