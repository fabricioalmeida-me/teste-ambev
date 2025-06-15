using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;

    public CreateCartHandler(
        ICartRepository cartRepository,
        ICartService cartService,
        IMapper mapper,
        ILogger<CreateCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _cartService = cartService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating cart for user {UserId}", request.UserId);

        var validator = new CreateCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for CreateCartCommand: {Errors}",
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            throw new ValidationException(validationResult.Errors);
        }

        var cart = new Cart(request.UserId, request.Date);
        
        cart = await _cartRepository.CreateAsync(cart, cancellationToken);
        
        foreach (var item in request.Products)
        {
            await _cartService.AddItemAsync(cart.Id, item.ProductId, item.Quantity, cancellationToken);
        }
        
        var updatedCart = await _cartRepository.GetByIdAsync(cart.Id, cancellationToken);

        return _mapper.Map<CreateCartResult>(updatedCart);
    }
}
