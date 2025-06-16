using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<(Guid, UpdateUserRequest), UpdateUserCommand>()
            .ConstructUsing(src => new UpdateUserCommand(
                src.Item1,
                src.Item2.Email,
                src.Item2.Username,
                src.Item2.Password,
                src.Item2.Phone,
                src.Item2.Firstname,
                src.Item2.Lastname,
                src.Item2.City,
                src.Item2.Street,
                src.Item2.Number,
                src.Item2.Zipcode,
                src.Item2.Latitude,
                src.Item2.Longitude,
                Enum.Parse<UserStatus>(src.Item2.Status, true),
                Enum.Parse<UserRole>(src.Item2.Role, true) 
            ));
    }
}