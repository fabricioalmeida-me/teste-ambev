using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts;
public class CartItem : BaseEntity
{
    public Guid CartId { get; private set; }
    public Cart Cart { get; private set; } = null!;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }

    public decimal Total => (UnitPrice * Quantity) - Discount;
    
    private CartItem() { }

    public CartItem(Guid cartId, Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (quantity > 20)
            throw new InvalidOperationException("Cannot purchase more than 20 items of the same product");

        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = CalculateDiscount();
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (quantity > 20)
            throw new InvalidOperationException("Cannot purchase more than 20 items of the same product");

        Quantity = quantity;
        Discount = CalculateDiscount();
    }

    private decimal CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            return UnitPrice * Quantity * 0.20m;

        if (Quantity >= 4)
            return UnitPrice * Quantity * 0.10m;

        return 0;
    }
}