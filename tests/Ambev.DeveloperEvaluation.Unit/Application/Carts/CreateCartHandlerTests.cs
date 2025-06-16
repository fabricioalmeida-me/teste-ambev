using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OneOf.Types;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _cartService = Substitute.For<ICartService>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateCartHandler>>();
        _handler = new CreateCartHandler(_cartRepository, _userRepository, _cartService, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid cart data When creating cart Then returns success result")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = CartTestData.GenerateValidCart(command.UserId);
        var user = UserTestData.GenerateValidUser();

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(cart);

        _cartRepository.GetByIdAsync(cart.Id, Arg.Any<CancellationToken>())
            .Returns(cart);

        var expectedResult = new CreateCartResult { Id = cart.Id };
        _mapper.Map<CreateCartResult>(cart).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsT0.Should().BeTrue();
        result.AsT0.Id.Should().Be(expectedResult.Id);
    }

    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var invalidCommand = new CreateCartCommand();

        // Act
        var act = async () => await _handler.Handle(invalidCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid cart data When handling Then calls AddItem for each product")]
    public async Task Handle_ValidRequest_CallsAddItemForEachProduct()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = CartTestData.GenerateValidCart(command.UserId);
        var user = UserTestData.GenerateValidUser();

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(cart);

        _cartRepository.GetByIdAsync(cart.Id, Arg.Any<CancellationToken>())
            .Returns(cart);

        _mapper.Map<CreateCartResult>(Arg.Any<Cart>()).Returns(new CreateCartResult { Id = cart.Id });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        foreach (var item in command.Products)
        {
            await _cartService.Received(1).AddItemAsync(cart.Id, item.ProductId, item.Quantity, Arg.Any<CancellationToken>());
        }
    }

    [Fact(DisplayName = "Given user not found When creating cart Then returns NotFound")]
    public async Task Handle_UserNotFound_ReturnsNotFound()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsT1.Should().BeTrue(); // T1 == NotFound
        result.AsT1.Should().BeOfType<NotFound>();
    }
}
