namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

public class GetCartByIdResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<GetCartByIdItemResponse> Products { get; set; } = new();
}