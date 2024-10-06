using Common.Application;
using CoreModule.Application.Category;
using CoreModule.Application.Category.Create;
using CoreModule.Application.Course;
using CoreModule.Application.Course.Create;
using CoreModule.Application.Teacher;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Courses.DomainServices;
using CoreModule.Domain.Teachers.DomainServices;
using CoreModule.Facade;
using CoreModule.Infrastructure;
using CoreModule.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Config;

public static class CoreModuleBootstrapper
{
    public static IServiceCollection InitCoreModule(this IServiceCollection service,IConfiguration configuration)
    {
        CoreModuleFacadeBootstrapper.RegisterDependency(service);
        CoreModuleInfrastructureBootstrapper.RegisterDependency(service,configuration);
        CoreModuleQueryBootstrapper.RegisterDependency(service,configuration);
        service.AddMediatR(typeof(CreateCourseCategoryCommand).Assembly);
        service.AddValidatorsFromAssembly(typeof(CreateCourseCommand).Assembly);
        service.RegisterCommonApplication();
        service.AddScoped<ICategoryDomainService, CourseCategoryDomainService>();
        service.AddScoped<ITeacherDomainService,TeacherDomainService>();
        service.AddScoped<ICourseDomainService,CourseDomainService>();
        return service;
    }
}