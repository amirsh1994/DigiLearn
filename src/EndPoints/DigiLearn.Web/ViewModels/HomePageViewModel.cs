using CoreModule.Query.Category._DTOs;

namespace DigiLearn.Web.ViewModels;

public class HomePageViewModel
{
    public List<CourseCardViewModel> LatestCourses { get; set; } = [];

}

public class CourseCardViewModel    
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public string ImageName { get; set; }

    public int Price { get; set; }

    public int VisitCount { get; set; }

    public string Duration { get; set; }

    public int CommentCount { get; set; }

    public string TeacherName { get; set; }
}