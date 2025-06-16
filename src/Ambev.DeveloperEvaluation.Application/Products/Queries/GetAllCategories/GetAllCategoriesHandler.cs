using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<GetAllCategoriesHandler> _logger;
    private readonly ICacheService _cacheService;
    private const string CacheKey = "products:categories";

    public GetAllCategoriesHandler(
        IProductRepository productRepository, 
        ILogger<GetAllCategoriesHandler> logger,
        ICacheService cacheService) 
    {
        _productRepository = productRepository;
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<GetAllCategoriesResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request to get all product categories.");
        
        var validator = new GetAllCategoriesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed when retrieving product categories: {Errors}",
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            
            throw new ValidationException(validationResult.Errors);
        }
        
        var cached = await _cacheService.GetAsync<GetAllCategoriesResult>(CacheKey, cancellationToken);
        if (cached is not null)
            return cached;

        var categories = await _productRepository.GetAllCategoriesAsync();
        var result = new GetAllCategoriesResult{ Categories = categories };
        
        await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        
        return result;
    }
}