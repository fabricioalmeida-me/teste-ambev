using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, OneOf<UpdateCartResult, NotFound>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;

    public UpdateCartHandler(
        ICartRepository cartRepository,
        IUserRepository userRepository,
        ICartService cartService,
        IMapper mapper,
        ILogger<UpdateCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _cartService = cartService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OneOf<UpdateCartResult, NotFound>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
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

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("User with ID {UserId} not found.", request.UserId);
            return new NotFound();
        }

        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart is null)
        {
            _logger.LogWarning("Cart with ID {CartId} not found.", request.Id);
            return new NotFound();
        }

        cart.SetUser(request.UserId);
        cart.SetDate(request.Date);
        cart.ClearItems();

        foreach (var item in request.Products)
        {
            await _cartService.AddItemAsync(cart.Id, item.ProductId, item.Quantity, cancellationToken);
        }

        var updatedCart = await _cartRepository.GetByIdAsync(cart.Id, cancellationToken);
        var result = _mapper.Map<UpdateCartResult>(updatedCart);

        return result;
    }
}
