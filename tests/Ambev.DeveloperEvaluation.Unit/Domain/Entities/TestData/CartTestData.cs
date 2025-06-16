using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Bogus;
using DomainCart = Ambev.DeveloperEvaluation.Domain.Entities.Carts.Cart;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CartTestData
{
    private static readonly Faker Faker = new();
    
    public static DomainCart GenerateValidCart(Guid? userId = null)
    {
        var cart = new DomainCart(userId ?? Guid.NewGuid(), DateTime.UtcNow);
        
        var itemCount = Faker.Random.Int(1, 5);
        for (int i = 0; i < itemCount; i++)
        {
            var productId = Guid.NewGuid();
            var quantity = Faker.Random.Int(1, 20);
            var unitPrice = Faker.Random.Decimal(10, 100);

            cart.AddItem(productId, quantity, unitPrice);
        }

        return cart;
    }
    
    public static CartItem GenerateValidCartItem(Guid? cartId = null)
    {
        var productId = Guid.NewGuid();
        var quantity = Faker.Random.Int(1, 20);
        var unitPrice = Faker.Random.Decimal(10, 100);

        return new CartItem(cartId ?? Guid.NewGuid(), productId, quantity, unitPrice);
    }
    
    public static DomainCart GenerateInvalidCartWithoutItems()
    {
        return new DomainCart(Guid.NewGuid(), DateTime.UtcNow);
    }
    
    public static CartItem GenerateInvalidCartItem()
    {
        var quantity = Faker.Random.Int(-10, 0);
        var unitPrice = Faker.Random.Decimal(10, 100);
        return new CartItem(Guid.NewGuid(), Guid.NewGuid(), quantity, unitPrice);
    }
}