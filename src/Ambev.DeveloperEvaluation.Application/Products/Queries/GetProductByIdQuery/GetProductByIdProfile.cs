using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProductsQuery;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdProfile : Profile
{
    public GetProductByIdProfile()
    {
        CreateMap<Product, GetProductByIdResult>();
    }
}