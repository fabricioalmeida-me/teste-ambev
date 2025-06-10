using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.ValueObject;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared;

public class SharedProductProfile : Profile
{
    public SharedProductProfile()
    {
        CreateMap<ProductRatingDto, ProductRating>();
    }
}