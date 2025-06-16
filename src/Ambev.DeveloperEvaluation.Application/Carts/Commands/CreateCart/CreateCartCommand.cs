using MediatR;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartCommand : IRequest<OneOf<CreateCartResult, NotFound>>
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CreateCartItemCommand> Products { get; set; } = new();
}
