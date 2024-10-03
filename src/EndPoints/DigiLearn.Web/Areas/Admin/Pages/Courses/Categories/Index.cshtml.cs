using CoreModule.Application.Category.Delete;
using CoreModule.Facade.Category;
using CoreModule.Query.Category._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories;

public class IndexModel(ICourseCategoryFacade categoryFacade) : BaseRazor
{
    public List<CourseCategoryDto> Categories { get; set; }

    public async Task OnGet()
    {
        Categories = await categoryFacade.GetMainCategories();

    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var result=await categoryFacade.Delete(id);
            return result;
        });
    }
}

