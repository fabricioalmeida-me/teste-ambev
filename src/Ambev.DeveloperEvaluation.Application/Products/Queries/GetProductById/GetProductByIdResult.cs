using Ambev.DeveloperEvaluation.Application.Products.Shared;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;

public class GetProductByIdResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ProductRatingDto Rating { get; set; } = new();
}