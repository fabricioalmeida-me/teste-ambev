using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCartCommand;

public class CreateCartResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CartItemResultDto> Products { get; set; } = new();
}