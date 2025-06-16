using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;

public static class CreateCartHandlerTestData
{
    private static readonly Faker<CreateCartItemCommand> ItemFaker = new Faker<CreateCartItemCommand>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20));

    private static readonly Faker<CreateCartCommand> CartCommandFaker = new Faker<CreateCartCommand>()
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.Date, f => f.Date.Recent())
        .RuleFor(c => c.Products, f => ItemFaker.Generate(f.Random.Int(1, 5)));

    public static CreateCartCommand GenerateValidCommand()
    {
        return CartCommandFaker.Generate();
    }
}