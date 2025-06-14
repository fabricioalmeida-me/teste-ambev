using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.DeleteCart;

public class DeleteCartCommand : IRequest<DeleteCartResult>
{
    public Guid Id { get; set; }

    public DeleteCartCommand(Guid id)
    {
        Id = id;
    }
}