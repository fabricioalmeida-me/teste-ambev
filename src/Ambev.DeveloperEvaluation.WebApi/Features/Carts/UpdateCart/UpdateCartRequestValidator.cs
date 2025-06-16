using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
    public UpdateCartRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Cart ID is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required.");

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product ID is required.");
            product.RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });
    }
}