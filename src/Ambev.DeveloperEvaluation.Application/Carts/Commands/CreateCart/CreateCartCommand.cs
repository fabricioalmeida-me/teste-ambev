using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartCommand : IRequest<CreateCartResult>
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CreateCartItemCommand> Products { get; set; } = new();
}
