using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategories;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[Authorize(Roles = "Admin, Manager")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        
        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully.",
            Data = _mapper.Map<CreateProductResponse>(result)
        });
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<UpdateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        
        return result.Match<IActionResult>(
            success => Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully.",
                Data = _mapper.Map<UpdateProductResponse>(success)
            }),
            notFound => NotFound(new ApiResponse
            {
                Success = false,
                Message = "Product not found."
            })
        );
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest() { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Match<IActionResult>(
            success => Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully."
            }),
            notFound => NotFound(new ApiResponse
            {
                Success = false,
                Message = "Product not found."
            })
        );
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest() { Id = id };
        var validator = new GetProductByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetProductByIdQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        
        return result.Match<IActionResult>(
            success => Ok(new ApiResponseWithData<GetProductByIdResponse>
            {
                Success = true,
                Message = "Product retrieved successfully.",
                Data = _mapper.Map<GetProductByIdResponse>(success)
            }),
            notFound => NotFound(new ApiResponse
            {
                Success = false,
                Message = "Product not found."
            })
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllProductsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsRequest request, 
        CancellationToken cancellationToken)
    {
        var validator = new GetAllProductsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetAllProductsQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<GetAllProductsResponse>>(result);

        return Ok(new ApiResponseWithData<List<GetAllProductsResponse>>
        {
            Success = true,
            Message = mapped.Any() 
                ? "Products retrieved successfully."
                : "No products found.",
            Data = mapped
        });
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetAllCategoriesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<GetAllCategoriesResponse>(result);
        
        return Ok(new ApiResponseWithData<GetAllCategoriesResponse>
        {
            Success = true,
            Message = mapped.Categories.Any()
            ? "Product categories retrieved successfully."
            : "No product categories found.",
            Data = mapped
        });
    }

    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetProductsByCategoryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductsByCategory(
        [FromRoute] string category,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string? order = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProductsByCategoryRequest
        {
            Category = category,
            Page = page,
            Size = size,
            Order = order
        };
        
        var validator = new GetProductsByCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetProductsByCategoryQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<GetProductsByCategoryResponse>>(result);

        return Ok(new ApiResponseWithData<List<GetProductsByCategoryResponse>>
        {
            Success = true,
            Message = mapped.Any()
                ? "Products retrieved successfully."
                : "No products found for the specified category.",
            Data = mapped
        });
    }
}