using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, OneOf<CreateCartResult, NotFound>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;

    public CreateCartHandler(
        ICartRepository cartRepository,
        IUserRepository userRepository,
        ICartService cartService,
        IMapper mapper,
        ILogger<CreateCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _cartService = cartService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OneOf<CreateCartResult, NotFound>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
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
        
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("User with ID {UserId} not found.", request.UserId);
            return new NotFound();
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
