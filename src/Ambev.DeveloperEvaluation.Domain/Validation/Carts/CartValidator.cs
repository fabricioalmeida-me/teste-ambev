using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Carts;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Cart must have at least one item.");
    }
}