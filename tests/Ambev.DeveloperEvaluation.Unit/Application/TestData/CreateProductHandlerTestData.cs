using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
    /// - Title
    /// - Description
    /// - Price
    /// - Category
    /// - Image (URL)
    /// - RatingRate (1 to 5)
    /// - RatingCount (0 to 1000)
    /// </summary>
    private static readonly Faker<CreateProductCommand> createProductFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Rating, f => new ProductRatingDto
        {
            Rate = f.Random.Decimal(1, 5),
            Count = f.Random.Int(0, 1000)
        });
    
    /// <summary>
    /// Generates a valid Product command with randomized data.
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateProductCommand with randomized test data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductFaker.Generate();
    }
}