using CoreModule.Application.Teacher.AcceptRequest;
using CoreModule.Application.Teacher.RejectRequest;
using CoreModule.Facade.Teacher;
using CoreModule.Query.Teacher._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers;

public class ShowModel(ITeacherFacade teacherFacade):BaseRazor
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


    public async Task<IActionResult> OnPostAccept(Guid teacherId)
    {
        

        return await AjaxTryCatch(async () =>
        {
            var result = await teacherFacade.AcceptRequest(new AcceptTeacherRequestCommand
            {
                TeacherId = teacherId
            });
            return result;
        });
    }


    public async Task<IActionResult> OnPostReject(Guid teacherId,string description)
    {
        var result = await teacherFacade.RejectRequest(new RejectTeacherRequestCommand
        {
            TeacherId = teacherId,
            Description = description
        });
        return RedirectAndShowAlert(result,RedirectToPage("show",new{teacherId}));
    }
}

