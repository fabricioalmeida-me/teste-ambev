using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CartItemDto> Products { get; set; } = new();
}