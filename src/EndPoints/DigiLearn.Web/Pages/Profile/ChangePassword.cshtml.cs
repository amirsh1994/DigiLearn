using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.ChangePassword;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

[BindProperties]
public class ChangePasswordModel(IUserFacade userFacade) : BaseRazor
{
    [Display(Name = " کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }


    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(NewPassword), ErrorMessage = "کلمه های عبور یکسان نیستند")]
    [DataType(DataType.Password)]
    public string ConfirmedNewPassword { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        var changePassword = await userFacade.ChangePassword(new ChangeUserPasswordCommand
        {
            UserId = User.GetUserId(),
            CurrentPassword = CurrentPassword,
            NewPassword = NewPassword
        });
        return RedirectAndShowAlert(changePassword, RedirectToPage("Index"));
    }
}

