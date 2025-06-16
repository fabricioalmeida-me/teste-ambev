using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Products;

public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }
    
    [Fact(DisplayName = "Invalid title formats should fail validation")]
    public void Given_InvalidTitle_When_Validated_Then_ShouldHaveError()
    {
        var product = ProductTestData.GenerateValidProduct();
        product.Title = "";

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Title);
    }

    [Theory(DisplayName = "Invalid price should fail validation")]
    [InlineData(0)]
    [InlineData(-10)]
    public void Given_InvalidPrice_When_Validated_Then_ShouldHaveError(decimal price)
    {
        var product = ProductTestData.GenerateValidProduct();
        product.Price = price;

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Price);
    }

    [Fact(DisplayName = "Invalid image URL should fail validation")]
    public void Given_InvalidImageUrl_When_Validated_Then_ShouldHaveError()
    {
        var product = ProductTestData.GenerateValidProduct();
        product.Image = "invalid-url";

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Image);
    }
}