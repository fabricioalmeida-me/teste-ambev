using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCarts;

public class GetAllCartsHandler : IRequestHandler<GetAllCartsQuery, IEnumerable<GetAllCartsResult>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCartsHandler> _logger;
    private readonly ICacheService _cacheService;
    private const string CacheKey = "carts:getall";

    public GetAllCartsHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<GetAllCartsHandler> logger,
        ICacheService cacheService)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<GetAllCartsResult>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request to get all carts. Page: {Page}, Size: {Size}, Order: {Order}",
            request.Page, request.Size, request.Order ?? "none");

        var validator = new GetAllCartsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed in GetAllCarts. Errors: {Errors}",
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            throw new ValidationException(validationResult.Errors);
        }

        var cached = await _cacheService.GetAsync<IEnumerable<GetAllCartsResult>>(CacheKey, cancellationToken);
        if (cached is not null)
            return cached;

        var carts = await _cartRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);
        var result = _mapper.Map<IEnumerable<GetAllCartsResult>>(carts);

        await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }
}
