using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}