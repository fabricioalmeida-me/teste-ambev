using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCartCommand;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartCommand : IRequest<CreateCartResult>
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CartItemCreateDto> Products { get; set; } = new();
}
