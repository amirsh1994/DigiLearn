using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using Common.Application;

namespace BlogModule.Services;

internal interface IBlogService
{
    Task<OperationResult> CreateCategory(CreateCategoryCommand command);

    Task<OperationResult> EditCategory(EditCategoryCommand command);

    Task<OperationResult> Delete(Guid categoryId);

    Task<BlogCategoryDto> GetCategoryById(Guid id);

    Task<List<BlogCategoryDto>> GetAll();
}

internal class BlogService(ICategoryRepository categoryRepository, IMapper mapper,IPostRepository postRepository) : IBlogService
{

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        if (await categoryRepository.ExistsAsync(x => x.slug == command.Slug))
        {
            return OperationResult.Error("slug is exists");
        }
        var cat = mapper.Map<Category>(command);
        categoryRepository.Add(cat);
        await categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        var category = await categoryRepository.GetAsync(command.Id);
        if (category == null)
        {
            return OperationResult.NotFound();
        }

        if (command.Slug != category.slug)
        {
            if (await categoryRepository.ExistsAsync(x => x.slug == command.Slug))
            {
                return OperationResult.Error("slug is exists");
            }
        }
        category.slug = command.Slug;
        category.Title = command.Title;
        categoryRepository.Update(category);
        await categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> Delete(Guid categoryId)
    {
        var category = await categoryRepository.GetAsync(categoryId);

        if (await postRepository.ExistsAsync(x=>x.CategoryId==categoryId))
        {
            return OperationResult.Error("این دسته بندی قبلا استفاده شده است لطفا پست های مربوطه را حذف کنید ودوباره امتحان نمایید.");
        }
        if (category == null)
        {
            return OperationResult.NotFound();
        }
        categoryRepository.Delete(category);
        await categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<BlogCategoryDto> GetCategoryById(Guid id)
    {
        var category = await categoryRepository.GetAsync(id);
        return mapper.Map<BlogCategoryDto>(category);
    }

    public async Task<List<BlogCategoryDto>> GetAll()
    {
        var categories = await categoryRepository.GetAll();
        return mapper.Map<List<BlogCategoryDto>>(categories);
    }
}

