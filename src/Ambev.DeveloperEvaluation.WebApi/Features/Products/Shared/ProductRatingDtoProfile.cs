using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;

public class ProductRatingDtoProfile : Profile
{
    public ProductRatingDtoProfile()
    {
        CreateMap<WebApi.Features.Products.Shared.ProductRatingDto, 
            Application.Products.Shared.ProductRatingDto>().ReverseMap();
    }
}