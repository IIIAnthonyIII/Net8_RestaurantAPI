namespace Restaurants.Application.Users;

// public class CurrentUser (Para crear el test cambiar a class)
public record CurrentUser (string Id, string Email, IEnumerable<string> Roles, 
    string? Nationality, DateOnly? DateOfBirth)
{
    public bool IsinRole(string role) => Roles.Contains(role);
}
