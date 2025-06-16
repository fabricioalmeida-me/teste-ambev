namespace Ambev.DeveloperEvaluation.Application.Carts.Services;

public interface ICartService
{
    Task AddItemAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken);
    Task RemoveItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken);
    Task UpdateItemQuantityAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken);
}