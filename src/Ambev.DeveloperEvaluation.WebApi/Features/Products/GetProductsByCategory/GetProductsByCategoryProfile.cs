using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryProfile : Profile
{
    public GetProductsByCategoryProfile()
    {
        CreateMap<GetProductsByCategoryResult, GetProductsByCategoryResponse>();
        CreateMap<GetProductsByCategoryRequest, GetProductsByCategoryQuery>();
        CreateMap<Domain.ValueObject.ProductRating, WebApi.Features.Products.Shared.ProductRatingDto>();
    }
}