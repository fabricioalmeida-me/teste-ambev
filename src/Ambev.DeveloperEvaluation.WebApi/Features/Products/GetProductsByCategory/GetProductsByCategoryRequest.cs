using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryRequest
{
    [FromRoute]
    public string Category { get; set; } = string.Empty;
    
    [FromQuery]
    public int Page { get; set; } = 1;
    
    [FromQuery]
    public int Size { get; set; } = 10;
    
    [FromQuery]
    public string? Order { get; set; }
}