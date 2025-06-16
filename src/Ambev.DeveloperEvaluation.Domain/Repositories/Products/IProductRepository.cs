using Ambev.DeveloperEvaluation.Domain.Entities.Products;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int page, int size, string? order, CancellationToken cancellationToken = default);
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetAllCategoriesAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page, int size, string? order, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id);
    Task<int> CountAsync();
}