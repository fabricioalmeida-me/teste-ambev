using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class CartItemUpdateValidator : AbstractValidator<CartItemUpdateDto>
{
    public CartItemUpdateValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(p => p.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}