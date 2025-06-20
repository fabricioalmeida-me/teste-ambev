using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.DeleteCart;

public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Cart ID is required.");
    }
}