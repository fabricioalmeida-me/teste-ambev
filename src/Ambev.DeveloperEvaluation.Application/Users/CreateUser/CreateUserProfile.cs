using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.ValueObject.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                new Name(src.FirstName, src.LastName)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                new Address
                {
                    City = src.City,
                    Street = src.Street,
                    Number = src.Number,
                    Zipcode = src.ZipCode,
                    Geolocation = new GeoLocation(src.Latitude, src.Longitude)
                }));

        CreateMap<User, CreateUserResult>();
    }
}
