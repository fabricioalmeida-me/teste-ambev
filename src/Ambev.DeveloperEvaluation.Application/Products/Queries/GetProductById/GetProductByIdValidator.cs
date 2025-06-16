using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty()
            .WithMessage("Product ID is required.");
    }
}