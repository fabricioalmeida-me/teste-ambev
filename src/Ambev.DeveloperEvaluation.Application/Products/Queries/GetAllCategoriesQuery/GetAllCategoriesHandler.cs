using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ICacheService _cacheService;
    private const string CacheKey = "products:categories";

    public GetAllCategoriesHandler(IProductRepository productRepository, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _cacheService = cacheService;
    }

    public async Task<GetAllCategoriesResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllCategoriesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var cached = await _cacheService.GetAsync<GetAllCategoriesResult>(CacheKey, cancellationToken);
        if (cached is not null)
            return cached;

        var categories = await _productRepository.GetAllCategoriesAsync();
        var result = new GetAllCategoriesResult{ Categories = categories };
        
        await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }
}