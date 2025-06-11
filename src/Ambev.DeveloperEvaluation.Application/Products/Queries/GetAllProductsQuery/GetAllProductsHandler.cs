using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProductsQuery;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private const string CacheKey = "products:getall";

    public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<GetAllProductsResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var cached = await _cacheService.GetAsync<IEnumerable<GetAllProductsResult>>(CacheKey, cancellationToken);
        if (cached is not null)
            return cached;
        
        var products = await _productRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);
        var result = _mapper.Map<IEnumerable<GetAllProductsResult>>(products);
        
        await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }
}