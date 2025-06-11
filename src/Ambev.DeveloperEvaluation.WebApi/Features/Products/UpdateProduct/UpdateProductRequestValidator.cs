using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(p => p.Id).NotEmpty();

        RuleFor(p => p.Title)
            .NotEmpty().MinimumLength(3).MaximumLength(100);

        RuleFor(p => p.Price)
            .GreaterThan(0);

        RuleFor(p => p.Description)
            .NotEmpty().MaximumLength(500);

        RuleFor(p => p.Category)
            .NotEmpty().MaximumLength(100);

        RuleFor(p => p.Image)
            .NotEmpty()
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Image must be a valid URL.");

        RuleFor(p => p.Rating)
            .SetValidator(new ProductRatingDtoValidator());
    }
}