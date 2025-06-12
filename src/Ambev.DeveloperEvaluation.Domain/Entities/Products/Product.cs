using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using Ambev.DeveloperEvaluation.Domain.ValueObject;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products;

public class Product : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public ProductRating Rating { get; set; } = new();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    public void Update(string title, decimal price, string description, string category, string image, decimal ratingRate, int ratingCount)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = new ProductRating
        {
            Rate = ratingRate,
            Count = ratingCount
        };
        UpdatedAt = DateTime.UtcNow;
    }
}