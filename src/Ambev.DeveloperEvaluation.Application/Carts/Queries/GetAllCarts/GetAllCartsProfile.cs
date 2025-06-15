using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetAllCarts;

public class GetAllCartsProfile : Profile
{
    public GetAllCartsProfile()
    {
        CreateMap<CartItem, GetAllCartsItemResult>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<Cart, GetAllCartsResult>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));
    }
}