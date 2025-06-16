using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(p => p.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}