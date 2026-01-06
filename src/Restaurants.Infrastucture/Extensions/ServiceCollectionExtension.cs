using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastucture.Authorization;
using Restaurants.Infrastucture.Authorization.Requirements;
using Restaurants.Infrastucture.Authorization.Services;
using Restaurants.Infrastucture.Configuration;
using Restaurants.Infrastucture.Persistence;
using Restaurants.Infrastucture.Repositories;
using Restaurants.Infrastucture.Seeders;
using Restaurants.Infrastucture.Storage;

namespace Restaurants.Infrastucture.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantDb");
        services.AddDbContext<RestaurantsDBContext>(options => 
            options.UseSqlServer(connectionString).EnableSensitiveDataLogging()); //Se habilita ver data
        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDBContext>();
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, 
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "French", "Italiano"))
            .AddPolicy(PolicyNames.AtLeast20, 
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreateAtLeast2Restaurants,
                builder => builder.AddRequirements(new CreateMultipleRestaurantsRequirement(2)));
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreateMultipleRestaurantsRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
