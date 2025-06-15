namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartItemCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}