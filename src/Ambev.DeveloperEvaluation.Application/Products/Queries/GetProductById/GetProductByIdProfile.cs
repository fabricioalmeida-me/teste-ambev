using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;

public class GetProductByIdProfile : Profile
{
    public GetProductByIdProfile()
    {
        CreateMap<Product, GetProductByIdResult>();
    }
}