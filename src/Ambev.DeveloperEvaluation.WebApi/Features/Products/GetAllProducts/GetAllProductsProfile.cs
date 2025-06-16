using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProducts;
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