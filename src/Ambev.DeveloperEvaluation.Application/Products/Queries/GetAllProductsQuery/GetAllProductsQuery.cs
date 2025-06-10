using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProductsQuery;

public class GetAllProductsQuery : IRequest<IEnumerable<GetAllProductsResult>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }

    public GetAllProductsQuery(int page, int size, string? order)
    {
        Page = page;
        Size = size;
        Order = order;
    }
}