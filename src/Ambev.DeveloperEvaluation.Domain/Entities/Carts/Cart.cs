using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Carts;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts;

public class Cart : BaseEntity
{
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public List<CartItem> Items { get; private set; } = new();

    public decimal Total => Items.Sum(i => i.Total);
    
    private Cart() { }

    public Cart(Guid userId, DateTime date)
    {
        UserId = userId;
        Date = date;
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (Items.Any(i => i.ProductId == productId))
            throw new InvalidOperationException("Product already exists in cart. Use UpdateItem to change quantity.");

        Items.Add(new CartItem(Id, productId, quantity, unitPrice));
    }

    public void UpdateItem(Guid productId, int quantity, decimal unitPrice)
    {
        var existing = Items.FirstOrDefault(i => i.ProductId == productId);
        if (existing == null)
            throw new InvalidOperationException("Product not found in cart.");

        Items.Remove(existing);
        Items.Add(new CartItem(Id, productId, quantity, unitPrice));
    }

    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
            Items.Remove(item);
    }

    public void ClearItems()
    {
        Items.Clear();
    }

    public void SetUser(Guid userId)
    {
        UserId = userId;
    }

    public void SetDate(DateTime date)
    {
        Date = date;
    }

    public ValidationResultDetail Validate()
    {
        var validator = new CartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
