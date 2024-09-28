using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Teacher._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel(ICourseFacade coursedFacade, ITeacherFacade teacherFacade) : BaseRazorFilter<CourseFilterParams>
{
    public CourseFilterResult FilterResult { get; set; }

    public TeacherDto Teacher { get; set; }

    public async Task OnGet()
    {
        var teacher = await teacherFacade.GetTeacherByUserId(User.GetUserId());
        FilterParams.TeacherId = teacher!.Id;
        FilterResult = await coursedFacade.GetCourseByFilter(FilterParams);
    }
}

