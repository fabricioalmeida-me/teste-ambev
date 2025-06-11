using Ambev.DeveloperEvaluation.Application.Caching;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductByIdHandler> _logger;
    private readonly ICacheService _cacheService;

    public GetProductByIdHandler(
        IProductRepository productRepository, 
        IMapper mapper, 
        ILogger<GetProductByIdHandler> logger,
        ICacheService cacheService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetProductById request. Product ID: {ProductId}", request.Id);
        
        var validator = new GetProductByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for GetProductById. ID: {ProductId}, Errors: {Errors}",
                request.Id,
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            throw new ValidationException(validationResult.Errors);
        }
        
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