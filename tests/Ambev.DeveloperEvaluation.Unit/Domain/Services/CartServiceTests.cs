using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class CartServiceTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _cartService = new CartService(_cartRepository, _productRepository);
    }

    [Fact(DisplayName = "Should add item to cart with discount applied")]
    public async Task Given_ValidData_When_AddItemAsync_Then_ItemIsAddedWithCorrectPrice()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Test Product",
            Price = 100m,
            Description = "Sample product"
        };
        var quantity = 10;
        var finalPrice = product.Price * 0.80m;

        _cartRepository.GetByIdAsync(cart.Id, Arg.Any<CancellationToken>()).Returns(cart);
        _productRepository.GetByIdAsync(product.Id, Arg.Any<CancellationToken>()).Returns(product);

        // Act
        await _cartService.AddItemAsync(cart.Id, product.Id, quantity, CancellationToken.None);

        // Assert
        await _cartRepository.Received(1).UpdateAsync(
            Arg.Is<Cart>(c =>
                c.Items.Exists(i =>
                    i.ProductId == product.Id &&
                    i.Quantity == quantity &&
                    i.UnitPrice == finalPrice)),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw if quantity exceeds 20")]
    public async Task Given_TooManyItems_When_AddItemAsync_Then_ShouldThrow()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var quantity = 21;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _cartService.AddItemAsync(cartId, productId, quantity, CancellationToken.None));
    }
}
