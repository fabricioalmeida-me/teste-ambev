using Ambev.DeveloperEvaluation.Domain.ValueObject;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Product;

public class ProductTests
{
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProduct_When_Validated_Then_ShouldBeValid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProduct_When_Validated_Then_ShouldBeInvalid()
    {
        // Arrange
        var product = new DeveloperEvaluation.Domain.Entities.Products.Product
        {
            Title = "", 
            Price = -10, 
            Description = "",
            Category = "", 
            Image = "", 
            Rating = new ProductRating
            {
                Rate = -1, 
                Count = -5 
            }
        };

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
    
    [Fact(DisplayName = "Update should change product values and update UpdatedAt")]
    public void Given_ValidData_When_Updated_Then_FieldsShouldChange()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var originalCreatedAt = product.CreatedAt;

        // Act
        product.Update(
            "Updated Title", 99.99m, "Updated Description", "Updated Category", "http://image.com/image.jpg", 4.5m, 100
        );

        // Assert
        Assert.Equal("Updated Title", product.Title);
        Assert.Equal(99.99m, product.Price);
        Assert.Equal("Updated Description", product.Description);
        Assert.Equal("Updated Category", product.Category);
        Assert.Equal("http://image.com/image.jpg", product.Image);
        Assert.Equal(4.5m, product.Rating.Rate);
        Assert.Equal(100, product.Rating.Count);
        Assert.NotNull(product.UpdatedAt);
        Assert.True(product.UpdatedAt > originalCreatedAt);
    }
}