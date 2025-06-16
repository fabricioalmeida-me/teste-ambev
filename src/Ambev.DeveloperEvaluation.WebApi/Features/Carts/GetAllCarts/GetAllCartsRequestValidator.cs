using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

public class GetAllCartsRequestValidator : AbstractValidator<GetAllCartsRequest>
{
    public GetAllCartsRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0.");
        RuleFor(x => x.Size).InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.");
    }
}