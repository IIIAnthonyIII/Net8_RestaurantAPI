﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser ();
}

public class UserContext (IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser ()
    {
        var user = (httpContextAccessor.HttpContext?.User)
            ?? throw new InvalidOperationException("User context no está presente");
        if (user.Identity == null || !user.Identity.IsAuthenticated) return null;
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateOfBirdString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var dateOfBird = (dateOfBirdString == null) 
            ? (DateOnly?)null 
            : DateOnly.ParseExact(dateOfBirdString, "yyyy-mm-dd");
        return new CurrentUser(userId, email, roles, nationality, dateOfBird);
    }
}
