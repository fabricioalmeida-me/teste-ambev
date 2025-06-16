using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategories;

public class GetAllCategoriesValidator : AbstractValidator<GetAllCategoriesQuery>
{
    public GetAllCategoriesValidator()
    {
    }
}