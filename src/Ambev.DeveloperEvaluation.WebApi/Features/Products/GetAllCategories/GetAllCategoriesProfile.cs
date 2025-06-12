using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllCategoriesQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllCategories;

public class GetAllCategoriesProfile : Profile
{
    public GetAllCategoriesProfile()
    {
        CreateMap<GetAllCategoriesResult, GetAllCategoriesResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
        
        CreateMap<List<string>, GetAllCategoriesResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src));
    }
}