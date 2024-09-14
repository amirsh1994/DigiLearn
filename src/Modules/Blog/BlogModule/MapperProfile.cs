using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;

namespace BlogModule;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<CreateCategoryCommand,Category>().ReverseMap();

        CreateMap<Category,BlogCategoryDto>().ReverseMap();
    }
}