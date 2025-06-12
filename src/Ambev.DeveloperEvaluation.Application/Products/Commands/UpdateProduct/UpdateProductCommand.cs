using System.Security.AccessControl;
using Ambev.DeveloperEvaluation.Application.Products.Shared;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public ProductRatingDto Rating { get; set; } = new();

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}