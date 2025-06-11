using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;

public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdRequestValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Product ID is required.");
    }
}