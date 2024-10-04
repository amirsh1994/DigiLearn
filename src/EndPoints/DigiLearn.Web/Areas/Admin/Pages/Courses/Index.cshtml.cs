using CoreModule.Facade.Course;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses;

public class IndexModel(ICourseFacade courseFacade) : BaseRazorFilter<CourseFilterParams>
{
    public CourseFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterResult = await courseFacade.GetCourseByFilter(FilterParams);
    }
}
