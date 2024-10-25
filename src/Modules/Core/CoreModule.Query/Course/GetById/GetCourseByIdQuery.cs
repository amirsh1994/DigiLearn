using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.GetById;

public record GetCourseByIdQuery(Guid Id):IBaseQuery<CourseDto?>;


internal class GetCourseByIdQueryHandler(QueryContext db):IBaseQueryHandler<GetCourseByIdQuery, CourseDto?>
{
    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await db.Courses
            .Include(x=>x.Sections)
            .ThenInclude(x=>x.Episodes)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (course == null)
        {
            return null;
        }

        return new CourseDto
        {
            Id = course.Id,
            CreationDate = course.CreationDate,
            TeacherId = course.TeacherId,
            CategoryId = course.CategoryId,
            SubCategoryId = course.SubCategoryId,
            Title = course.Title,
            Slug = course.Slug,
            Description = course.Description,
            ImageName = course.ImageName,
            VideoName = course.VideoName,
            Price = course.Price,
            LastUpdate = course.LastUpdate,
            SeoData = course.SeoData,
            CourseLevel = course.CourseLevel,
            CourseStatus = course.CourseStatus,
            Status = course.Status,
            Sections = course.Sections.Select(x=>new CourseSectionDto
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                CourseId = x.CourseId,
                Title = x.Title,
                DisplayOrder = x.DisplayOrder,
                Episodes =x.Episodes.Select(e=>new EpisodeDto
                {
                    Id =e.Id ,
                    CreationDate = e.CreationDate,
                    SectionId = e.SectionId,
                    Title = e.Title,
                    EnglishTitle = e.EnglishTitle,
                    Token = e.Token,
                    TimeSpan = e.TimeSpan,
                    VideoName = e.VideoName,
                    AttachmentName = e.AttachmentName,
                    IsActive = e.IsActive,
                    IsFree = e.IsFree,
                }).ToList() 
            }).ToList()
        };
    }
}