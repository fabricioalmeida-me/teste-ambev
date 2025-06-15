using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Carts;

public class CartTests
{
    [Fact(DisplayName = "Cart should be valid when created with user and date")]
    public void Given_ValidUserIdAndDate_When_CartCreated_Then_CartShouldBeValid()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();

        // Act
        var result = cart.Validate();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Cart should throw when adding the same product twice")]
    public void Given_CartWithItem_When_AddSameProduct_Then_ShouldThrow()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var productId = Guid.NewGuid();
        cart.AddItem(productId, 1, 10);

        // Act
        Action act = () => cart.AddItem(productId, 1, 10);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Product already exists in cart. Use UpdateItem to change quantity.");
    }

    [Fact(DisplayName = "CartItem discount should be 0 when quantity < 4")]
    public void Given_QuantityLessThan4_When_CartItemCreated_Then_DiscountShouldBeZero()
    {
        // Arrange
        var item = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 3, 100);

        // Act & Assert
        item.Discount.Should().Be(0);
    }

    [Fact(DisplayName = "CartItem discount should be 10% when quantity is between 4 and 9")]
    public void Given_QuantityBetween4And9_When_CartItemCreated_Then_DiscountShouldBe10Percent()
    {
        // Arrange
        var item = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 5, 100);

        // Act
        var expectedDiscount = 100 * 5 * 0.10m;

        // Assert
        item.Discount.Should().Be(expectedDiscount);
    }

    [Fact(DisplayName = "CartItem discount should be 20% when quantity is between 10 and 20")]
    public void Given_QuantityBetween10And20_When_CartItemCreated_Then_DiscountShouldBe20Percent()
    {
        // Arrange
        var item = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 15, 100);

        // Act
        var expectedDiscount = 100 * 15 * 0.20m;

        // Assert
        item.Discount.Should().Be(expectedDiscount);
    }

    [Fact(DisplayName = "CartItem should throw when quantity is greater than 20")]
    public void Given_QuantityGreaterThan20_When_CartItemCreated_Then_ShouldThrow()
    {
        // Act
        Action act = () => new CartItem(Guid.NewGuid(), Guid.NewGuid(), 25, 100);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot purchase more than 20 items of the same product");
    }

    [Fact(DisplayName = "CartItem should throw when quantity is zero or less")]
    public void Given_QuantityZero_When_CartItemCreated_Then_ShouldThrow()
    {
        // Act
        Action act = () => new CartItem(Guid.NewGuid(), Guid.NewGuid(), 0, 100);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Quantity must be greater than zero");
    }
}
