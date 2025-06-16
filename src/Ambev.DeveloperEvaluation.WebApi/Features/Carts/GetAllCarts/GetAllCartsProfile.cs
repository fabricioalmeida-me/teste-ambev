using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

public class GetAllCartsProfile : Profile
{
    public GetAllCartsProfile()
    {
        CreateMap<GetAllCartsRequest, GetAllCartsQuery>();
        CreateMap<GetAllCartsResult, GetAllCartsResponse>();
        CreateMap<GetAllCartsItemResult, GetAllCartsItemResponse>();
    }
}