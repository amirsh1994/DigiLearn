﻿using Common.Query;
using Common.Query.Filter;
using CoreModule.Domain.Courses.Enums;

namespace CoreModule.Query.Course._DTOs;

public class CourseFilterParams:BaseFilterParam
{
    public Guid ? TeacherId { get; set; }

    public CourseActionStatus ? ActionStatus { get; set; }

}
public class CourseFilterData:BaseDto
{
    public string ImageName { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public int Price { get; set; }

    public int EpisodeCount { get; set; }

    public CourseActionStatus CourseStatus { get; set; }
}


public class CourseFilterResult:BaseFilter<CourseFilterData>
{

}