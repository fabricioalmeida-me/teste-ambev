using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartValidator : AbstractValidator<CreateCartCommand>
{
    public CreateCartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.Products)
            .NotEmpty().WithMessage("At least one product is required.");

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            product.RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });
    }
}