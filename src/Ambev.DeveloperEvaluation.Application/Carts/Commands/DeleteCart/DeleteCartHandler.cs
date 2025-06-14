using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ILogger<DeleteCartHandler> _logger;

    public DeleteCartHandler(ICartRepository cartRepository, ILogger<DeleteCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _logger = logger;
    }

    public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling delete cart request. CartId: {CartId}", request.Id);

        var validator = new DeleteCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for DeleteCartCommand. Errors: {Errors}",
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));

            throw new ValidationException(validationResult.Errors);
        }

        var cartExists = await _cartRepository.ExistsAsync(request.Id, cancellationToken);
        if (!cartExists)
        {
            _logger.LogWarning("Cart with ID {CartId} not found.", request.Id);
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");
        }

        var success = await _cartRepository.DeleteAsync(request.Id, cancellationToken);
        var message = success ? "Cart successfully deleted." : "Failed to delete the cart.";

        _logger.LogInformation("Cart deletion status for ID {CartId}: {Status}", request.Id, message);

        return new DeleteCartResult
        {
            Success = success,
            Message = message
        };
    }
}