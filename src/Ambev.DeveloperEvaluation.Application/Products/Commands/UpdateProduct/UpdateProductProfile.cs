using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<Product, UpdateProductResult>();
    }
}