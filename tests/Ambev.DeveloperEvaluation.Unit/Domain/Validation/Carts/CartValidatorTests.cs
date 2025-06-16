using Ambev.DeveloperEvaluation.Domain.Validation.Carts;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class CartValidatorTests
{
    private readonly CartValidator _validator = new();

    [Fact(DisplayName = "Cart with valid data should pass validation")]
    public void Given_ValidCart_When_Validated_Then_ShouldNotHaveErrors()
    {
        var cart = CartTestData.GenerateValidCart();
        var result = _validator.TestValidate(cart);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Cart without user should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var cart = CartTestData.GenerateValidCart();
        cart.SetUser(Guid.Empty);

        var result = _validator.TestValidate(cart);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact(DisplayName = "Cart with empty items should fail validation")]
    public void Given_CartWithoutItems_When_Validated_Then_ShouldHaveError()
    {
        var cart = CartTestData.GenerateInvalidCartWithoutItems();
        var result = _validator.TestValidate(cart);
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact(DisplayName = "Cart with empty date should fail validation")]
    public void Given_CartWithoutDate_When_Validated_Then_ShouldHaveError()
    {
        var cart = CartTestData.GenerateValidCart();
        cart.SetDate(default);

        var result = _validator.TestValidate(cart);
        result.ShouldHaveValidationErrorFor(x => x.Date);
    }
}