using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

public class GetCartByIdRequestValidator : AbstractValidator<GetCartByIdRequest>
{
    public GetCartByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required.");
    }
}