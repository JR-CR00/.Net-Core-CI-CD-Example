using Mapster;
using DotNetApiExampleCICD.Models;
using DotNetApiExampleCICD.Models.Dto;

namespace DotNetApiExampleCICD.Mapping;

public class CategoryProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDto>().TwoWays();
        config.NewConfig<Category, CreateCategoryDto>().TwoWays();
    }
}
