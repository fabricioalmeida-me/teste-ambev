using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCartsQuery;

public class GetAllCartsValidator : AbstractValidator<GetAllCartsQuery>
{
    public GetAllCartsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than zero.");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.");
    }
}