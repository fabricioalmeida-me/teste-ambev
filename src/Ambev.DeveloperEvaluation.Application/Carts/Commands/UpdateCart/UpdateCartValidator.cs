using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Cart ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleForEach(x => x.Products).SetValidator(new UpdateCartItemValidator());
    }
}