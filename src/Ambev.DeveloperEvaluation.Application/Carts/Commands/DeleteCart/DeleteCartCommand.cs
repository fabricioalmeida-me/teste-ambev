using MediatR;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.DeleteCart;

public class DeleteCartCommand : IRequest<OneOf<DeleteCartResult, NotFound>>
{
    public Guid Id { get; set; }

    public DeleteCartCommand(Guid id)
    {
        Id = id;
    }
}