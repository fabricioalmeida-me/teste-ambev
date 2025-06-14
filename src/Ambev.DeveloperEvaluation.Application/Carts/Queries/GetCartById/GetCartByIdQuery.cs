using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdQuery : IRequest<GetCartByIdResult>
{
    public Guid Id { get; set; }

    public GetCartByIdQuery(Guid id)
    {
        Id = id;
    }
}