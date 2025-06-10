using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProductsQuery;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllProductsResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var products = await _productRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);
        return _mapper.Map<IEnumerable<GetAllProductsResult>>(products);
    }
}