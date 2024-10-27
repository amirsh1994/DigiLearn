using System.ComponentModel.DataAnnotations;
using BlogModule.Services;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Areas.Admin.Pages.Blog.Categories;

[BindProperties]
public class IndexModel(IBlogService blogService,IRenderViewToString renderViewToString) : BaseRazor
{

    public List<BlogCategoryDto> Categories { get; set; }



    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    public async Task OnGet()
    {
        var blogCategories = await blogService.GetAllCategories();
        Categories = blogCategories;
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var blogCategory=await blogService.GetCategoryById(id);
            if (blogCategory==null)
            {
                return OperationResult<string>.NotFound();
            }

            var viewString =await renderViewToString.RenderToStringAsync("_Edit", new EditBlogCategoryCommand
            {
                Id = blogCategory.Id,
                Title = blogCategory.Title,
                Slug = blogCategory.Slug
            }, PageContext);
            return OperationResult<string>.Success(viewString);
        });
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {

        return await AjaxTryCatch(() => blogService.Delete(id));
    }

    public async Task<IActionResult> OnPostEdit(EditBlogCategoryCommand command)
    {

        return await AjaxTryCatch(() => blogService.EditCategory(command));
    }

    public async Task<IActionResult> OnPost()
    {
        return await AjaxTryCatch(()=>blogService.CreateCategory(new CreateBlogCategoryCommand
        {
            Title = Title,
            Slug = Slug
        }));
    }
}

