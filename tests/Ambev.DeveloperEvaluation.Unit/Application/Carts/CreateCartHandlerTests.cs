using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _cartService = Substitute.For<ICartService>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateCartHandler>>();
        _handler = new CreateCartHandler(_cartRepository, _cartService, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid cart data When creating cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var createdCart = CartTestData.GenerateValidCart(command.UserId);

        var result = new CreateCartResult { Id = createdCart.Id };

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(createdCart);

        _cartRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(createdCart);

        _mapper.Map<CreateCartResult>(Arg.Any<Cart>()).Returns(result);

        // Act
        var createCartResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        createCartResult.Should().NotBeNull();
        createCartResult.Id.Should().Be(createdCart.Id);
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var invalidCommand = new CreateCartCommand();

        // Act
        var act = () => _handler.Handle(invalidCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid cart data When handling Then calls AddItem for each product")]
    public async Task Handle_ValidRequest_CallsAddItemForEachProduct()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var createdCart = CartTestData.GenerateValidCart(command.UserId);

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(createdCart);

        _cartRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(createdCart);

        _mapper.Map<CreateCartResult>(Arg.Any<Cart>()).Returns(new CreateCartResult { Id = createdCart.Id });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        foreach (var item in command.Products)
        {
            await _cartService.Received(1).AddItemAsync(createdCart.Id, item.ProductId, item.Quantity, Arg.Any<CancellationToken>());
        }
    }
}
