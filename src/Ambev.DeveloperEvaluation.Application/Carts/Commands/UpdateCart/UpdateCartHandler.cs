using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;

    public UpdateCartHandler(
        ICartRepository cartRepository,
        ICartService cartService,
        IMapper mapper,
        ILogger<UpdateCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _cartService = cartService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateCartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating cart with ID {CartId}", request.Id);

        var validator = new UpdateCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateCartCommand: {Errors}",
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            throw new ValidationException(validationResult.Errors);
        }

        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");
        
        cart.SetUser(request.UserId);
        cart.SetDate(request.Date);
        cart.ClearItems();
        
        foreach (var item in request.Products)
        {
            await _cartService.AddItemAsync(cart.Id, item.ProductId, item.Quantity, cancellationToken);
        }
        
        var updatedCart = await _cartRepository.GetByIdAsync(cart.Id, cancellationToken);

        return _mapper.Map<UpdateCartResult>(updatedCart);
    }
}
