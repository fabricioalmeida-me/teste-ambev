using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategoryQuery;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, List<GetProductsByCategoryResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<GetProductsByCategoryResult>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = await _productRepository.GetByCategoryAsync(
            request.Category,
            request.Page,
            request.Size,
            request.Order,
            cancellationToken
        );
        
        return _mapper.Map<List<GetProductsByCategoryResult>>(products);
    }
}