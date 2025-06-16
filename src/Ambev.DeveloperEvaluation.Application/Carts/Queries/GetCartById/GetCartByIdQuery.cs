using MediatR;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdQuery : IRequest<OneOf<GetCartByIdResult, NotFound>>
{
    public Guid Id { get; set; }

    public GetCartByIdQuery(Guid id)
    {
        Id = id;
    }
}