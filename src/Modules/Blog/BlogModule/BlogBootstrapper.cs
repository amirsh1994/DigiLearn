using BlogModule.Context;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogModule;

public static class BlogBootstrapper
{
    public static IServiceCollection InitBlogModules(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext<BlogContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("Blog_Context"));
        });
        service.AddScoped<IBlogService, BlogService>();

        service.AddScoped<ICategoryRepository,CategoryRepository>();

        service.AddScoped<IPostRepository,PostRepository>();

        service.AddAutoMapper(typeof(MapperProfile).Assembly);

        return service;
    }
}