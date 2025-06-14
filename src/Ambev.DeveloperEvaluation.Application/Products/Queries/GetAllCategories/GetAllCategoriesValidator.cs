using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesValidator : AbstractValidator<GetAllCategoriesQuery>
{
    public GetAllCategoriesValidator()
    {
    }
}