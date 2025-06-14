using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCartsQuery.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCartsQuery;

public class GetAllCartsResult
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CartItemDto> Products { get; set; } = new();
}