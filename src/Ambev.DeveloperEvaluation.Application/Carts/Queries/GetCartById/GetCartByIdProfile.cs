using Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries.GetCartById;

public class GetCartByIdProfile : Profile
{
    public GetCartByIdProfile()
    {
        CreateMap<Cart, GetCartByIdResult>();
        CreateMap<CartItem, CartItemDto>();
    }
}