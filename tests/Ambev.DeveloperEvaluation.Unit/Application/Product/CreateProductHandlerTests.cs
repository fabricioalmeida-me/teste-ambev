using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObject;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product;

public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new DeveloperEvaluation.Domain.Entities.Products.Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            Image = command.Image,
            Rating = new ProductRating { Rate = command.Rating.Rate, Count = command.Rating.Count },
            CreatedAt = DateTime.UtcNow
        };
        var result = new CreateProductResult { Id = product.Id };
        
        _mapper.Map<DeveloperEvaluation.Domain.Entities.Products.Product>(command).Returns(product);
        _productRepository.CreateAsync(product, Arg.Any<CancellationToken>()).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);
        
        var handler = new CreateProductHandler(_productRepository, _mapper);
        
        // Act
        var response = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(product.Id);
        await _productRepository.Received(1).CreateAsync(product, Arg.Any<CancellationToken>());
    }
    
    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var invalidCommand = new CreateProductCommand();
        var handler = new CreateProductHandler(_productRepository, _mapper);

        // Act
        var act = () => handler.Handle(invalidCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Arrange
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new DeveloperEvaluation.Domain.Entities.Products.Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            Image = command.Image,
            Rating = new ProductRating { Rate = command.Rating.Rate, Count = command.Rating.Count }
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Products.Product>(command).Returns(product);
        _productRepository.CreateAsync(product, Arg.Any<CancellationToken>()).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(new CreateProductResult { Id = product.Id });

        var handler = new CreateProductHandler(_productRepository, _mapper);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _mapper.Received(1).Map<DeveloperEvaluation.Domain.Entities.Products.Product>(Arg.Is<CreateProductCommand>(c =>
            c.Title == command.Title &&
            c.Price == command.Price &&
            c.Description == command.Description &&
            c.Category == command.Category &&
            c.Image == command.Image &&
            c.Rating.Rate == command.Rating.Rate &&
            c.Rating.Count == command.Rating.Count));
    }
}