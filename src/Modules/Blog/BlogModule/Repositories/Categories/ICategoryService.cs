using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Repositories.Categories;

internal interface ICategoryRepository:IBaseRepository<Category>
{
    void Delete(Category category);

    Task<List<Category>> GetAll();
}

internal class CategoryRepository(BlogContext context):BaseRepository<Category,BlogContext>(context),ICategoryRepository
{
    public void Delete(Category category)
    {
        context.Categories.Remove(category);
    }

    public async Task<List<Category>> GetAll()
    {
        var categories = await context.Categories.ToListAsync();
        return categories;
    }
}