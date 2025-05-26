namespace Restaurants.Application.Users;

public record CurrentUser (string Id, string Email, IEnumerable<string> Roles)
{
    public bool IsinRole(string role) => Roles.Contains(role);
}
