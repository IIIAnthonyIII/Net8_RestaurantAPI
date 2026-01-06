namespace Restaurants.Domain.Entities;
public class Restaurant
{
    public int Id { get; set; }
    //Con Default el valor es obligatorio (Not Null)
    public string Name { get; set; } = default!;
    public string Description { get; set; } =  default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    //Con ? es opcional (Null)
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = new();
    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public string? LogoUrl { get; set; }
}
