using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

public class GetCartByIdProfile : Profile
{
    public GetCartByIdProfile()
    {
        CreateMap<GetCartByIdRequest, GetCartByIdQuery>();
        CreateMap<GetCartByIdResult, GetCartByIdResponse>();
    }
}