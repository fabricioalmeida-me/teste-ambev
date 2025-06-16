using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObject.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, OneOf<UpdateUserResult, NotFound>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(IUserRepository userRepository, ILogger<UpdateUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<OneOf<UpdateUserResult, NotFound>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user with ID: {UserId}", request.Id);

        var user = new User
        {
            Id = request.Id,
            Email = request.Email,
            Username = request.Username,
            Phone = request.Phone,
            Password = request.Password,
            Status = request.Status,
            Role = request.Role,
            UpdatedAt = DateTime.UtcNow,
            Name = new Name(request.FirstName, request.LastName),
            Address = new Address(
                request.City,
                request.Street,
                request.Number,
                request.ZipCode,
                new GeoLocation(request.Latitude, request.Longitude)
            )
        };

        var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);
        if (updatedUser is null)
            return new NotFound();

        return new UpdateUserResult(updatedUser.Id);
    }
}