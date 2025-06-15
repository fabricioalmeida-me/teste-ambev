using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, GetCartByIdResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartByIdHandler> _logger;
    private readonly ICacheService _cache;

    public GetCartByIdHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<GetCartByIdHandler> logger,
        ICacheService cache)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _logger = logger;
        _cache = cache;
    }

    public async Task<GetCartByIdResult> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching cart with ID {CartId}", request.Id);

        var validator = new GetCartByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for cart ID {CartId}: {Errors}", request.Id,
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));

            throw new ValidationException(validationResult.Errors);
        }

        var cacheKey = $"carts:{request.Id}";
        var cached = await _cache.GetAsync<GetCartByIdResult>(cacheKey, cancellationToken);
        // if (cached is not null)
        //     return cached;

        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart is null)
        {
            _logger.LogWarning("Cart not found for ID {CartId}", request.Id);
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");
        }

        var result = _mapper.Map<GetCartByIdResult>(cart);

        await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);

        return result;
    }
}
