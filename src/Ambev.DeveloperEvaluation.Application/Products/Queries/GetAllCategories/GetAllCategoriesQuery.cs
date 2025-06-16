using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<GetAllCategoriesResult>, IRequest<IEnumerable<GetAllCategoriesResult>>;