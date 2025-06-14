namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CartItemResultDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}