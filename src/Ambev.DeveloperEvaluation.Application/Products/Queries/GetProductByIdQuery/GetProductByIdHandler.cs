using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var cacheKey = $"products:{request.Id}";
        var cached = await _cacheService.GetAsync<GetProductByIdResult>(cacheKey, cancellationToken);
        if (cached is not null)
            return cached;
        
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        
        var result = _mapper.Map<GetProductByIdResult>(product);
        
        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }
}