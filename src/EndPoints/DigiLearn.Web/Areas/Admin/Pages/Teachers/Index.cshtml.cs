using CoreModule.Application.Teacher.ToggleStatus;
using CoreModule.Facade.Teacher;
using CoreModule.Query.Teacher._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers;

public class IndexModel(ITeacherFacade teacherFacade):BaseRazor
{

    public List<TeacherDto> Teachers { get; set; }

    public async Task OnGet()
    {
        Teachers = await teacherFacade.GetTeacherList();
    }


    public async Task<IActionResult> OnPostToggleStatus(Guid teacherId)
    {
        return await AjaxTryCatch(async () =>
        {
            var result = await teacherFacade.ToggleStatus(new ToggleTeacherStatusCommand(teacherId));
            return result;
        });
    }
}

