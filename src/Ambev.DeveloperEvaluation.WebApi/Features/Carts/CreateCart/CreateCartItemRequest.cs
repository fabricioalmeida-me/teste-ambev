namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}