using MediatR;
using OneOf.Types;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<OneOf<GetProductByIdResult, NotFound>>;