using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, OneOf<GetAllUsersResult, NotFound>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<GetAllUsersHandler> _logger;

    public GetAllUsersHandler(IUserRepository userRepository, ILogger<GetAllUsersHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<OneOf<GetAllUsersResult, NotFound>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all users. Page: {Page}, Size: {Size}, Order: {Order}", request.Page, request.Size, request.Order);

        var (users, totalItems, totalPages) = await _userRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);

        if (!users.Any())
            return new NotFound();

        return new GetAllUsersResult(users.ToList(), totalItems, request.Page, totalPages);
    }
}