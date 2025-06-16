using MediatR;
using OneOf;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public record GetAllUsersQuery(
    int Page = 1,
    int Size = 10,
    string? Order = null
) : IRequest<OneOf<GetAllUsersResult, NotFound>>;