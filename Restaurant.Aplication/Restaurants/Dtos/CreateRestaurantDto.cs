using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Dtos;
public class CreateRestaurantDto
{
    [StringLength(100, MinimumLength =6)]
    public string Name { get; set; } = default!;
    [Required(ErrorMessage ="Descripción no puede estar vacio")]
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    [EmailAddress(ErrorMessage = "Email no es valido")] //Solo valida @
    public string? ContactEmail { get; set; }
    [Phone(ErrorMessage = "Teléfono no es valido")] //Solo valida numeros
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Código postal no es valido (XX-XXX)")]
    public string? PostalCode { get; set; }
}
