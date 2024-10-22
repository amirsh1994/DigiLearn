using Common.Query;
using Common.Query.Filter;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Query._Data.Entities;

namespace CoreModule.Query.Course._DTOs;

public class CourseFilterParams:BaseFilterParam
{
    public Guid ? TeacherId { get; set; }

    public CourseActionStatus ? ActionStatus { get; set; }

    public CourseFilterSort CourseFilterSort { get; set; } = CourseFilterSort.Latest;


}
public class CourseFilterData:BaseDto
{
    public string ImageName { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string TeacherName { get; set; }

    public int Price { get; set; }

    public int EpisodeCount => Sections.Sum(x => x.Episodes.Count);

    public CourseActionStatus CourseStatus { get; set; }

    public List<CourseSectionDto> Sections { get; set; }

    public string GetDuration()
    {
        var totalSeconds = 0;
        var totalMinutes = 0;
        var totalHours = 0;
        foreach (var section in Sections)
        {
            foreach (var item in section.Episodes)
            {
                totalSeconds += item.TimeSpan.Seconds;
                totalMinutes += item.TimeSpan.Minutes;
                totalHours += item.TimeSpan.Hours;
            }

            while (totalSeconds > 60)
            {
                totalMinutes += 1;
                totalSeconds -= 60;
            }
            while (totalMinutes >= 60)
            {
                totalHours += 1;
                totalMinutes -= 60;
            }
        }
        return $"{totalHours:00} : {totalMinutes:00} : {totalSeconds:00}";
    }
}

public enum CourseFilterSort
{
    Latest,
    Oldest,
    Expensive,
    Cheapest
}


public class CourseFilterResult:BaseFilter<CourseFilterData>
{

}