using CoreModule.Facade.Category;
using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Facade;

public static class CoreModuleFacadeBootstrapper
{
    public static IServiceCollection RegisterDependency( IServiceCollection service)
    {
        service.AddScoped<ITeacherFacade, TeacherFacade>();
        service.AddScoped<ICourseCategoryFacade,CourseCategoryFacade>();
        service.AddScoped<ICourseFacade,CourseFacade>();

        return service;
    }
}