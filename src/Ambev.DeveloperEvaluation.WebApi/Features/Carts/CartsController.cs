using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Commands.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.Commands.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{    
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
    
        var command = _mapper.Map<CreateCartCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
    
        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully.",
            Data = _mapper.Map<CreateCartResponse>(result)
        });
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
    
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
    
        var command = _mapper.Map<DeleteCartCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);
    
        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart deleted successfully."
        });
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCart(
        [FromRoute] Guid id,
        [FromBody] UpdateCartRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
    
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
    
        var command = _mapper.Map<UpdateCartCommand>(request);
        command.Id = id;
    
        var result = await _mediator.Send(command, cancellationToken);
    
        return Ok(new ApiResponseWithData<UpdateCartResponse>
        {
            Success = true,
            Message = "Cart updated successfully.",
            Data = _mapper.Map<UpdateCartResponse>(result)
        });
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllCartsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllCarts(
        [FromQuery] GetAllCartsRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new GetAllCartsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
    
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
    
        var query = _mapper.Map<GetAllCartsQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<GetAllCartsResponse>>(result);
    
        return Ok(new ApiResponseWithData<List<GetAllCartsResponse>>
        {
            Success = true,
            Message = mapped.Any()
                ? "Carts retrieved successfully."
                : "No carts found.",
            Data = mapped
        });
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCartByIdRequest { Id = id };
        var validator = new GetCartByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
    
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
    
        var query = _mapper.Map<GetCartByIdQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
    
        if (result == null)
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Cart with ID {id} not found."
            });
    
        return Ok(new ApiResponseWithData<GetCartByIdResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully.",
            Data = _mapper.Map<GetCartByIdResponse>(result)
        });
    }
}