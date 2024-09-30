using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel(ICourseFacade courseFacade, ITeacherFacade teacherFacade) : BaseRazor
{
    public CourseDto Course { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await courseFacade.GetCourseById(courseId);
        var teacher = await teacherFacade.GetTeacherByUserId(User.GetUserId());
        if (course == null || course.TeacherId!=teacher!.Id)
        {
            return RedirectToPage("../Index");
        }
        Course = course;
        return Page();
    }
}

