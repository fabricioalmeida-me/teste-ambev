namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

public class GetAllCartsRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
}