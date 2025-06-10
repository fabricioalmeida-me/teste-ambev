using Ambev.DeveloperEvaluation.Domain.ValueObject;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategoryQuery;

public class GetProductsByCategoryResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ProductRating Rating { get; set; } = new();
}