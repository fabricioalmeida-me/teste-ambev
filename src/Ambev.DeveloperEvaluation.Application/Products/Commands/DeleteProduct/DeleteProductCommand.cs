using MediatR;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<OneOf<DeleteProductResponse, NotFound>>;