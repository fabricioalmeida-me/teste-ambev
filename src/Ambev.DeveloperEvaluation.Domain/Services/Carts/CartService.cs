using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task AddItemAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken)
    {
        ValidateQuantityRules(quantity);

        var cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken)
                   ?? throw new KeyNotFoundException($"Cart with ID {cartId} not found.");

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken)
                      ?? throw new KeyNotFoundException($"Product with ID {productId} not found.");

        var finalPrice = ApplyDiscount(product.Price, quantity);

        cart.AddItem(productId, quantity, finalPrice);
        await _cartRepository.UpdateAsync(cart, cancellationToken);
    }

    public async Task RemoveItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken)
                   ?? throw new KeyNotFoundException($"Cart with ID {cartId} not found.");

        cart.RemoveItem(productId);
        await _cartRepository.UpdateAsync(cart, cancellationToken);
    }

    public async Task UpdateItemQuantityAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken)
    {
        ValidateQuantityRules(quantity);

        var cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken)
                   ?? throw new KeyNotFoundException($"Cart with ID {cartId} not found.");

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken)
                      ?? throw new KeyNotFoundException($"Product with ID {productId} not found.");

        var finalPrice = ApplyDiscount(product.Price, quantity);

        cart.UpdateItem(productId, quantity, finalPrice);
        await _cartRepository.UpdateAsync(cart, cancellationToken);
    }

    private static void ValidateQuantityRules(int quantity)
    {
        if (quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");
    }

    private static decimal ApplyDiscount(decimal basePrice, int quantity)
    {
        if (quantity >= 10)
            return basePrice * 0.80m;
        else if (quantity >= 4)
            return basePrice * 0.90m;
        else
            return basePrice;
    }
}
