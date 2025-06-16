using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(
    string Category,
    int Page = 1,
    int Size = 10,
    string? Order = null
) : IRequest<List<GetProductsByCategoryResult>>;