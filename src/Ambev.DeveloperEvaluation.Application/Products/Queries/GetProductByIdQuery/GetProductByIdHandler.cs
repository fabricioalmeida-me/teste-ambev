using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        
        return mapper.Map<GetProductByIdResult>(product);
    }
}