using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdValidator : AbstractValidator<GetCartByIdQuery>
{
    public GetCartByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Cart ID must not be empty.");
    }
}