using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;

public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResult>;