using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Category;
using CoreModule.Facade.Course;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.ViewModels;

namespace DigiLearn.Web.Infrastructure.Services;

public interface IHomePageService
{
    Task<HomePageViewModel> GetData();
}


public class HomePageService(ICourseFacade courseFacade):IHomePageService
{
    public async Task<HomePageViewModel> GetData()
    {
        
        var courses = await courseFacade.GetCourseByFilter(new CourseFilterParams
        {
            PageId = 1,
            Take = 8,
            ActionStatus = CourseActionStatus.Active,
            CourseFilterSort = CourseFilterSort.Latest
        });
        var homePageViewModel = new HomePageViewModel()
        {
            LatestCourses = courses.Data.Select(x=>new CourseCardViewModel
            {
                Title = x.Title,
                Slug = x.Slug,
                ImageName = x.ImageName,
                Price = x.Price,
                VisitCount = 0,
                Duration = "",
                CommentCount = 0,
                TeacherName = x.TeacherName,
            }).ToList()
        };
        return homePageViewModel;
    }
}