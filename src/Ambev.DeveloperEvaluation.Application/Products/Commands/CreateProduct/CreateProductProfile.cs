using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResult>();
    }
}