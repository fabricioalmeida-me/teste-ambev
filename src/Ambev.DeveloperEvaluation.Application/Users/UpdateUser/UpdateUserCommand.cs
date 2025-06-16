using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string Email,
    string Username,
    string Password,
    string Phone,
    string FirstName,
    string LastName,
    string City,
    string Street,
    int Number,
    string ZipCode,
    string Latitude,
    string Longitude,
    UserStatus Status,
    UserRole Role
) : IRequest<OneOf<UpdateUserResult, NotFound>>;