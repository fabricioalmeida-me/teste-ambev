using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;

public class ProductRatingDtoValidator : AbstractValidator<ProductRatingDto>
{
    public ProductRatingDtoValidator()
    {
        RuleFor(r => r.Rate)
            .InclusiveBetween(0, 5)
            .WithMessage("Rate must be between 0 and 5.");

        RuleFor(r => r.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating count must be zero or greater.");
    }
}