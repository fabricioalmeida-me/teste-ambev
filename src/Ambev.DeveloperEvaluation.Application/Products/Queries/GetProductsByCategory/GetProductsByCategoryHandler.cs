using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategoryQuery;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, List<GetProductsByCategoryResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductsByCategoryHandler> _logger;
    private readonly ICacheService _cacheService;

    public GetProductsByCategoryHandler(
        IProductRepository productRepository, 
        IMapper mapper,
        ILogger<GetProductsByCategoryHandler> logger,
        ICacheService cacheService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<List<GetProductsByCategoryResult>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Handling request to get products by category. Category: {Category}, Page: {Page}, Size: {Size}, Order: {Order}",
            request.Category, request.Page, request.Size, request.Order ?? "none");
        
        var validator = new GetProductsByCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for GetProductsByCategory. Errors: {Errors}",
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            throw new ValidationException(validationResult.Errors);
        }
        
        var cacheKey = $"products:category:{request.Category.ToLower()}:{request.Page}:{request.Size}:{request.Order?.ToLower()}";
        var cached = await _cacheService.GetAsync<List<GetProductsByCategoryResult>>(cacheKey, cancellationToken);
        if (cached is not null)
            return cached;

        var products = await _productRepository.GetByCategoryAsync(
            request.Category,
            request.Page,
            request.Size,
            request.Order,
            cancellationToken
        );
        
        var result = _mapper.Map<List<GetProductsByCategoryResult>>(products);
        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);

        return result;
    }
}