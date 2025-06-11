using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategoryQuery;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, List<GetProductsByCategoryResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<List<GetProductsByCategoryResult>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
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