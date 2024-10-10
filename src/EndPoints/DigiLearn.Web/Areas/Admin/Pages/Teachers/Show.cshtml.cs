using CoreModule.Facade.Teacher;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers;

public class ShowModel(ITeacherFacade teacherFacade):PageModel
{
    public TeacherDto Teacher { get; set; }

    public async Task<IActionResult> OnGet(Guid teacherId)
    {
        var teacher = await teacherFacade.GetTeacherById(teacherId);

        if (teacher==null)
        {
            return RedirectToPage("Index");
        }

        Teacher = teacher;
        return Page();
    }
}

