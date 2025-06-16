using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Address.Geolocation.Long));
    }
}
