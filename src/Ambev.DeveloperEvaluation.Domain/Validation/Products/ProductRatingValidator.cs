using Ambev.DeveloperEvaluation.Domain.ValueObject;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Products;

public class ProductRatingValidator : AbstractValidator<ProductRating>
{
    public ProductRatingValidator()
    {
        RuleFor(r => r.Rate)
            .InclusiveBetween(0, 5)
            .WithMessage("Rate must be between 0 and 5.");

        RuleFor(r => r.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating count must be zero or greater.");
    }
}