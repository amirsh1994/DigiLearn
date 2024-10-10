using CoreModule.Facade.Teacher;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers;

public class IndexModel(ITeacherFacade teacherFacade) : PageModel
{

    public List<TeacherDto> Teachers { get; set; }

    public async Task OnGet()
    {
        Teachers = await teacherFacade.GetTeacherList();
    }

}

