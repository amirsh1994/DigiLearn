using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Common.Application.SecurityUtil;
using DigiLearn.Web.Infrastructure.JwtUtil;
using DigiLearn.Web.Infrastructure.RazorUtils;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth;

[BindProperties]
public class LoginModel(IUserFacade userFacade, IConfiguration configuration) : BaseRazor
{
    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }

    [Display(Name = "رمزعبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; }

    public bool IsRememberMe { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        var user = await userFacade.GetUserByPhoneNumber(PhoneNumber);
        if (user == null)
        {
            ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }
        var isCompare = Sha256Hasher.IsCompare(user.password, Password);
        if (isCompare == false)
        {
            ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }
        var token = JwtTokenBuilder.BuildToken(user, configuration);
        HttpContext.Response.Cookies.Append("Token", token, new CookieOptions()
        {
            HttpOnly = true
            ,
            Expires = DateTimeOffset.Now.AddDays(7)
            ,
            Secure = true
        });

        return RedirectToPage("../Index");
    }
}

