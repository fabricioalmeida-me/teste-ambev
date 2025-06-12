using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResponse>;