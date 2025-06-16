using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, OneOf<DeleteProductResponse, NotFound>>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductHandler> _logger;

    public DeleteProductHandler(IProductRepository productRepository, ILogger<DeleteProductHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<OneOf<DeleteProductResponse, NotFound>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete product with ID: {ProductId}", command.Id);
        
        var validator = new DeleteProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed while deleting product with ID: {ProductId}. Errors: {Errors}",
                command.Id,
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            
            throw new ValidationException(validationResult.Errors);
        }
        
        var success = await _productRepository.DeleteAsync(command.Id, cancellationToken);

        if (!success)
        {
            _logger.LogWarning("Product not found. Unable to delete product with ID: {ProductId}", command.Id);
            return new NotFound();
        }
        
        return new DeleteProductResponse { Success = success };
    }
}