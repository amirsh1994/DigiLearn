using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Course;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Pages;

public class CoursesModel(ICourseFacade courseFacade):BaseRazorFilter<CourseFilterParams>
{
    public CourseFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterParams.ActionStatus = CourseActionStatus.Active;
        FilterParams.TeacherId = null;
        var result = await courseFacade.GetCourseByFilter(FilterParams);
        FilterResult = result;

    }
}

