using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCartsQuery;

public class GetAllCartsQuery : IRequest<IEnumerable<GetAllCartsResult>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }

    public GetAllCartsQuery(int page, int size, string? order)
    {
        Page = page;
        Size = size;
        Order = order;
    }
}