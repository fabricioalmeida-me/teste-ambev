using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResult>
{
    private readonly IProductRepository _productRepository;

    public GetAllCategoriesHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllCategoriesResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllCategoriesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var categories = await _productRepository.GetAllCategoriesAsync();
        
        return new GetAllCategoriesResult{ Categories = categories };
    }
}