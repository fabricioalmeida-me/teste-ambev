using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdProfile : Profile
{
    public GetCartByIdProfile()
    {
        CreateMap<CartItem, GetCartByIdItemResult>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<Cart, GetCartByIdResult>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));
    }
}