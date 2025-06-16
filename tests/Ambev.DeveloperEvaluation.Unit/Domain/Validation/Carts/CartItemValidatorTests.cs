using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Validation.Carts;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class CartItemValidatorTests
{
    private readonly CartItemValidator _validator = new();

    [Fact(DisplayName = "CartItem with valid data should pass validation")]
    public void Given_ValidCartItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        var item = CartTestData.GenerateValidCartItem();
        var result = _validator.TestValidate(item);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "CartItem with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
    {
        var item = CartTestData.GenerateValidCartItem();
        item.GetType().GetProperty("ProductId")!.SetValue(item, Guid.Empty);

        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact(DisplayName = "CartItem with zero unit price should fail validation")]
    public void Given_ZeroUnitPrice_When_Validated_Then_ShouldHaveError()
    {
        var item = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 2, 0);
        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }
}