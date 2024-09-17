using System.ComponentModel.DataAnnotations;
using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth;

[BindProperties]
public class RegisterModel(IUserFacade userFacade):BaseRazor
{
    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }

    [Display(Name = "رمزعبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5,ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; }

    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password),ErrorMessage = "کلمه عبور صحیح نمی باشد")]
    public string ConfirmedPassword { get; set; }


    public async Task OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result =await userFacade.RegisterUser(new RegisterUserCommand
        {
            PhoneNumber = PhoneNumber,
            Password = Password
        });
        if (result.Status==OperationResultStatus.Success)
        {
            result.Message = "ثبت نام شما با موفقیت انجام شد";
        }
        return RedirectAndShowAlert(result, RedirectToPage("Login"));
    }
}

