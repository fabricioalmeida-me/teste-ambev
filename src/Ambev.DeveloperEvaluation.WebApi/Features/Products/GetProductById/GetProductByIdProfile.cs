using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;

public class GetProductByIdProfile : Profile
{
    public GetProductByIdProfile()
    {
        CreateMap<GetProductByIdRequest, GetProductByIdQuery>();
        CreateMap<GetProductByIdResult, GetProductByIdResponse>();
    }
}