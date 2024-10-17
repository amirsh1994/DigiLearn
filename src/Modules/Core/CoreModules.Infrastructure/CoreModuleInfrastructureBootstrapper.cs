using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Teachers.DomainServices;
using CoreModule.Domain.Teachers.Repository;
using CoreModule.Infrastructure.EventHandlers;
using CoreModule.Infrastructure.Persistence;
using CoreModule.Infrastructure.Persistence.Category;
using CoreModule.Infrastructure.Persistence.Course;
using CoreModule.Infrastructure.Persistence.Teacher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Infrastructure;

public static class CoreModuleInfrastructureBootstrapper
{
    public static void RegisterDependency(IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<CoreModuleEfContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("CoreModule_Context"));
        });
        service.AddScoped<ICourseRepository, CourseRepository>();
        service.AddScoped<ITeacherRepository, TeacherRepository>();
        service.AddScoped<ICourseCategoryRepository,CourseCategoryRepository>();
        service.AddHostedService<UserRegisteredEventHandler>();
    }
}