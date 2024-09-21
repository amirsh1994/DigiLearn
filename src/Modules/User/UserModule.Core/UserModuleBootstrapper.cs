using AngleSharp.Dom;
using Common.Application;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;
using UserModule.Data;

namespace UserModule.Core;

public static class UserModuleBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext<UserContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("User_Context"));
        });
        service.AddAutoMapper(typeof(MapperProfile).Assembly);

        service.AddScoped<IUserFacade,UserFacade>();

        service.AddScoped<INotificationFacade,NotificationFacade>();

        service.AddValidatorsFromAssembly(typeof(RegisterUserValidator).Assembly);

        service.AddMediatR(typeof(UserModuleBootstrapper).Assembly);

        service.RegisterCommonApplication();
        return service;
    }
}