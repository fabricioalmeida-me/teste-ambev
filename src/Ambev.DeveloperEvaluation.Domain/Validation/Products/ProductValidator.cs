using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.ValueObject;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Products;

public class ProductValidator: AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Title must be no more than 50 characters.");
        
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(3).WithMessage("Description must be at least 3 characters.")
            .MaximumLength(500).WithMessage("Description must be no more than 500 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
        
        RuleFor(p => p.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category must be no more than 50 characters.");
        
        RuleFor(p => p.Image)
            .NotEmpty().WithMessage("Image is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Image must be a valid URL.");
        
        RuleFor(p => p.Rating)
            .SetValidator(new ProductRatingValidator());
    }
}