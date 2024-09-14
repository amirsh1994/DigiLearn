using BlogModule.Context;
using BlogModule.Services;
using BlogModule.Services.Categories;
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
        service.AddScoped<IBlogFacade, BlogFacade>();
        service.AddScoped<ICategoryService,CategoryService>();

        return service;
    }
}