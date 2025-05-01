using AutoMapper;
namespace Restaurants.Application.Restaurants.Dtos;
public class RestaurantProfile : Profile
{
    public RestaurantProfile ()
    {
        CreateMap<Domain.Entities.Restaurant, RestaurantDto>()
            .ForMember(x => x.City, opt => 
                opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(x => x.PostalCode, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(x => x.Street, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(x => x.City, opt =>
                opt.MapFrom(src => src.Dishes));
    }
}
