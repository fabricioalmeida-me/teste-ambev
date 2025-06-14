namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class CartItemUpdateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}