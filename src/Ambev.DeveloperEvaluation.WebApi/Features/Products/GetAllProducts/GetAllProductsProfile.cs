using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProductsQuery;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

public class GetAllProductsProfile : Profile
{
    public GetAllProductsProfile()
    {
        CreateMap<GetAllProductsRequest, GetAllProductsQuery>();
        CreateMap<GetAllProductsResult, GetAllProductsResponse>();
    }
}