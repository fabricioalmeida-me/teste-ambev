namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartItemCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}