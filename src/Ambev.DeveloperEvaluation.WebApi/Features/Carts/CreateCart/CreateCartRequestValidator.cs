using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    public CreateCartRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductId).NotEmpty();
            product.RuleFor(p => p.Quantity).GreaterThan(0);
        });
    }
}