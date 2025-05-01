using AutoMapper;
using Restaurants.Domain.Entities;
namespace Restaurants.Application.Dishes.Dtos;
public class DishProfile : Profile
{
    public DishProfile ()
    {
        CreateMap<Dish, DishDto>();
    }
}
