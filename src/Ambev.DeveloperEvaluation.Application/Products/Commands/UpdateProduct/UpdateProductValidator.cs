using Ambev.DeveloperEvaluation.Application.Products.Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty();

        RuleFor(p => p.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Price)
            .GreaterThan(0);

        RuleFor(p => p.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(p => p.Category)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Image)
            .NotEmpty()
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Image must be a valid URL.");

        RuleFor(p => p.Rating)
            .SetValidator(new ProductRatingDtoValidator());
    }
}