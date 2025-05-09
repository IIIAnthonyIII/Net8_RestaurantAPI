using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile ()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(x => x.City, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(x => x.PostalCode, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(x => x.Street, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Street));
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(x => x.Address, opt =>
                opt.MapFrom(src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));
        CreateMap<UpdateRestaurantCommand, Restaurant>();
    }
}
