using CoreModule.Facade.Category;
using CoreModule.Query.Category._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories;

public class IndexModel(ICourseCategoryFacade categoryFacade) : BaseRazor
{
    public List<CourseCategoryDto> Categories { get; set; }

    public async Task OnGet()
    {
        Categories = await categoryFacade.GetMainCategories();

    }
}

