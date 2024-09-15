using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using BlogModule.Utils;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;

namespace BlogModule.Services;

internal interface IBlogService
{
    Task<OperationResult> CreateCategory(CreateCategoryCommand command);

    Task<OperationResult> EditCategory(EditCategoryCommand command);

    Task<OperationResult> Delete(Guid categoryId);

    Task<BlogCategoryDto> GetCategoryById(Guid id);

    Task<List<BlogCategoryDto>> GetAllCategories();

    Task<OperationResult> CreatePost(CreatePostCommand command);

    Task<OperationResult> EditPost(EditPostCommand command);

    Task<OperationResult> DeletePost(EditPostCommand command);

    Task<BlogPostDto?> GetPostById(Guid postId);

}

internal class BlogService(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository, ILocalFileService fileService) : IBlogService
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

        if (await postRepository.ExistsAsync(x => x.CategoryId == categoryId))
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

    public async Task<List<BlogCategoryDto>> GetAllCategories()
    {
        var categories = await categoryRepository.GetAll();
        return mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = mapper.Map<Post>(command);
        if (await postRepository.ExistsAsync(x => x.Slug == command.Slug))
        {
            return OperationResult.Error("slug is already exists in post");
        }
        if (command.ImageFile.IsImage() == false)
        {
            return OperationResult.Error("عکس نا معتبر می باشد");
        }
        var imageName = await fileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
        post.ImageName = imageName;
        post.Visit = 1;
        post.Description = post.Description.SanitizeText();
        postRepository.Add(post);
        await postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditPost(EditPostCommand command)
    {
        var oldPost = await postRepository.GetTracking(command.Id);

        if (oldPost == null) return OperationResult.NotFound();

        if (command.Slug != oldPost.Slug)
        {
            if (await postRepository.ExistsAsync(x => x.Slug == command.Slug))
            {
                return OperationResult.Error("slug is already exists");
            }
        }

        if (command.ImageFile != null)
        {
            if (command.ImageFile.IsImage() == false)
            {
                return OperationResult.Error("عکس نا معتبر است");
            }
            var imageName = await fileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
            oldPost.ImageName = imageName;
        }

        oldPost.Description = command.Description.SanitizeText();
        oldPost.OwnerName = command.OwnerName;
        oldPost.Title = command.Title;
        oldPost.Slug = command.Slug;
        oldPost.UserId = command.UserId;
        oldPost.CategoryId = command.CategoryId;
        await postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeletePost(EditPostCommand command)
    {
        var post = await postRepository.GetAsync(command.Id);
        if (post == null) return OperationResult.NotFound();
        postRepository.Delete(post);
        await postRepository.Save();
        fileService.DeleteFile(BlogDirectories.PostImage, post.ImageName);
        return OperationResult.Success();
    }

    public async Task<BlogPostDto?> GetPostById(Guid postId)
    {
      var post=await postRepository.GetAsync(postId);
      if (post == null)
          return null;
      return mapper.Map<BlogPostDto>(post);
    }
}

