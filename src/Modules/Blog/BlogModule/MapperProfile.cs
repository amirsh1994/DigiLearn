using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;

namespace BlogModule;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Category,CreateCategoryCommand>().ReverseMap();

        CreateMap<Category,BlogCategoryDto>().ReverseMap();

        CreateMap<Post,CreatePostCommand>().ReverseMap();

        CreateMap<Post,BlogPostDto>().ReverseMap();
    }
}