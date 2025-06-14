namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCartsQuery.Dtos;

public class CartItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}