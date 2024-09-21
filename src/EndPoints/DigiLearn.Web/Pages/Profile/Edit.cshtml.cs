using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

[BindProperties]
public class EditModel(IUserFacade userFacade) : BaseRazor
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }


    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }


    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public async Task OnGet()
    {
        var user = await userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        if (user!=null)
        {
            Name = user.Name;
            Family = user.Family;
            Email = user.Email;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var result =await userFacade.EditUserProfile(new EditUserCommand
        {
            UserId = User.GetUserId(),
            Name = Name,
            Family = Family,
            Email = Email
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

