using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int page, int size, string? order, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(order))
        {
            query = ApplyOrdering(query, order);
        }
        
        return await query
            .Skip((page - 1) * size)
            .Take(size).AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product == null) return false;
        
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IEnumerable<string>> GetAllCategoriesAsync()
    { 
        return await _context.Products
            .AsNoTracking()
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page, int size, string? order,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Where(p => p.Category.ToLower() == category.ToLower());

        if (!string.IsNullOrWhiteSpace(order))
        {
            query = ApplyOrdering(query, order);
        }
        
        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Products.CountAsync();
    }
    
    private IQueryable<Product> ApplyOrdering(IQueryable<Product> query, string order)
    {
        var orders = order.Split(',');

        foreach (var item in orders)
        {
            var trimmed = item.Trim();
            var descending = trimmed.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
            var prop = trimmed.Split(' ')[0];
            
            query = prop.ToLower() switch
            {
                "title" => descending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "category" => descending ? query.OrderByDescending(p => p.Category) : query.OrderBy(p => p.Category),
                _ => query
            };
        }

        return query;
    }
}