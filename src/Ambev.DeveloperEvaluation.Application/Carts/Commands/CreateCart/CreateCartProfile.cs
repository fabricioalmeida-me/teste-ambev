using Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCartCommand;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>();
        CreateMap<CartItemCreateDto, CartItem>();
        CreateMap<Cart, CreateCartResult>();
        CreateMap<CartItem, CartItemResultDto>();
    }
}