namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;

public class UpdateCartItemResult
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}