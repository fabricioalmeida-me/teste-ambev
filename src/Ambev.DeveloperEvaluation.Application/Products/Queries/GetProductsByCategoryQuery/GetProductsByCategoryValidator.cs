using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategoryQuery;

public class GetProductsByCategoryValidator : AbstractValidator<GetProductsByCategoryQuery>
{
    public GetProductsByCategoryValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100)
            .WithMessage("Size must be between 1 and 100");
    }
}