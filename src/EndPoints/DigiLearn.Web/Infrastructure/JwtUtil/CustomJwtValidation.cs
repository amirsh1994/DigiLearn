using Microsoft.AspNetCore.Authentication.JwtBearer;
using UserModule.Core.Services;

namespace DigiLearn.Web.Infrastructure.JwtUtil;

public class CustomJwtValidation
{
    private readonly IUserFacade _userFacade;

    public CustomJwtValidation(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        //var userId = context.Principal.GetUserId();
        //var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");//این تو درخواست کاربر ارسال میشه
        //var existsToken =await _userFacade.GetUserTokenByJwtToken(jwtToken);
        //if (existsToken==null)
        //{
        //    context.Fail("Token is not valid And Not Found this error was in CustomJwtValidation in namespace Shop.Api.Infrastructure.JwtUtil;");
        //    return;
        //}
        //var user = await _userFacade.GetUserById(userId);
        //if (user==null|| user.IsActive==false)
        //{
        //    context.Fail("user is not active  this error was in CustomJwtValidation in namespace Shop.Api.Infrastructure.JwtUtil;");
        //}

    }
}