using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting update for product with ID: {ProductId}", command.Id);
        
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for product update. ID: {ProductId}, Errors: {Errors}",
                command.Id,
                string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
            throw new ValidationException(validationResult.Errors);
        }
        
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with ID: {ProductId} not found for update.", command.Id);
            throw new InvalidOperationException($"Product with ID {command.Id} not found.");
        }
        
        product.Update(
            command.Title,
            command.Price,
            command.Description,
            command.Category,
            command.Image,
            command.Rating.Rate,
            command.Rating.Count);
        
        var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);
        
        return _mapper.Map<UpdateProductResult>(updatedProduct);
    }
}