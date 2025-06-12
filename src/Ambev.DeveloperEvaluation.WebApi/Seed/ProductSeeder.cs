using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.ValueObject;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Seed;

public static class ProductSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        if (await context.Products.AnyAsync())
            return;

        var products = Enumerable.Range(1, 100).Select(i => new Product
        {
            Title = $"Product {i}",
            Description = $"Description for Product {i}",
            Category = i % 2 == 0 ? "electronics" : "clothing",
            Price = 10m * i,
            Image = $"https://example.com/product{i}.png",
            Rating = new ProductRating
            {
                Rate = Math.Round(3.5m + (i % 3) * 0.5m, 1),
                Count = 10 * i
            }
        }).ToList();

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}