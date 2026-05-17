using Mapster;
using DotNetApiExampleCICD.Models;
using DotNetApiExampleCICD.Models.Dto;

namespace DotNetApiExampleCICD.Mapping;

public class ProductProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .TwoWays();

        config.NewConfig<Product, CreateProductDto>().TwoWays();
        config.NewConfig<Product, UpdateProductDto>().TwoWays();
    }
}
