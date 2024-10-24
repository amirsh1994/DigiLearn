using CoreModule.Facade.Course;
using CoreModule.Query.Course._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class CourseModel(ICourseFacade courseFacade) : PageModel
{
    public CourseDto? Course { get; set; }

    public async Task<IActionResult> OnGet(string slug)
    {
        var course = await courseFacade.GetCourseBySlug(slug);

        if (course == null)
        {
            return NotFound();
        }

        Course = course;
        return Page();
    }
}

