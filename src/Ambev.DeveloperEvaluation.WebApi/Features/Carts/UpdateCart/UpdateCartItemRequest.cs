namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}