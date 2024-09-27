using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Teacher.Register;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Facade.Teacher;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages.Profile;

[BindProperties]
public class RegisterTeacherModel(ITeacherFacade teacherFacade) : BaseRazor
{
    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string UserName { get; set; }

    [Display(Name = "رزومه (پسوند بهتر است pdf باشد)")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile CvfileName { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var teacher = await teacherFacade.GetTeacherByUserId(User.GetUserId());
        if (teacher != null)
        {
            if (teacher.Status is TeacherStatus.Active or TeacherStatus.InActive)
            {
                ErrorAlert("شما قبلا ثبت نام کرده اید");
            }
            else
            {
                SuccessAlert("درخواست شما در حال بررسی می باشد.");
            }
        }

        return Page();
    }
    public async Task<IActionResult> OnPost()
    {
        var result = await teacherFacade.Register(new RegisterTeacherCommand()
        {
            UserId = User.GetUserId(),
            CvFileName = CvfileName,
            UserName = UserName
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

