using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Sections;

public class IndexModel(ICourseFacade courseFacade) : BaseRazor
{
    public CourseDto Course { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await courseFacade.GetCourseById(courseId);
        if (course == null)
        {
            return RedirectToPage("../Index");
        }
        Course = course;
        return Page();
    }
}


