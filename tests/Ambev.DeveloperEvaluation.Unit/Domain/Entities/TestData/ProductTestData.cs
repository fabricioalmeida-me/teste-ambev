using Ambev.DeveloperEvaluation.Domain.ValueObject;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData
{
    private static readonly Faker<DeveloperEvaluation.Domain.Entities.Products.Product> ProductFaker = new Faker<DeveloperEvaluation.Domain.Entities.Products.Product>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Rating, f => new ProductRating
        {
            Rate = f.Random.Decimal(0, 5),
            Count = f.Random.Number(0, 1000)
        });

    public static DeveloperEvaluation.Domain.Entities.Products.Product GenerateValidProduct() => ProductFaker.Generate();

    public static string GenerateInvalidTitle() => "";
    public static decimal GenerateInvalidPrice() => -10;
    public static ProductRating GenerateInvalidRating() => new() { Rate = -1, Count = -5 };
}