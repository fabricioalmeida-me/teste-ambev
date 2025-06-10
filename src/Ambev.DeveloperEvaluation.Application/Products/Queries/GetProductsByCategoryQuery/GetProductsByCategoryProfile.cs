using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductsByCategoryQuery;

public class GetProductsByCategoryProfile : Profile
{
    public GetProductsByCategoryProfile()
    {
        CreateMap<Product, GetProductsByCategoryResult>();
    }
}