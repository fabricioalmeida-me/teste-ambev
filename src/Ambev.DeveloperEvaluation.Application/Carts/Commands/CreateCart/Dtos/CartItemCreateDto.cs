namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CartItemCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}