using CoreModule.Domain.Teachers.Enums;
using CoreModule.Facade.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserModule.Data.Entities.Users;

namespace DigiLearn.Web.Infrastructure;

public class TeacherActionFilter(ITeacherFacade teacherFacade) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
        {
            context.Result = new RedirectResult("/");
        }

        var teacher = await teacherFacade.GetTeacherByUserId(context.HttpContext.User.GetUserId());
        if (teacher is not { Status: TeacherStatus.Active })
        {
            context.Result = new RedirectResult("/Profile");
        }
        await next();
    }
}