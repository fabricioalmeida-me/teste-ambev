using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategoriesQuery;

public record GetAllCategoriesQuery() : IRequest<GetAllCategoriesResult>, IRequest<IEnumerable<GetAllCategoriesResult>>;