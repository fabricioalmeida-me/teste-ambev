using MediatR;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartCommand : IRequest<OneOf<UpdateCartResult, NotFound>>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }

    public List<UpdateCartItemCommand> Products { get; set; } = new();
}