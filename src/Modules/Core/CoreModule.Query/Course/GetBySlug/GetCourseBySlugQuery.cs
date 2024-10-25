using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.Category._DTOs;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.GetBySlug;

public record GetCourseBySlugQuery(string Slug) : IBaseQuery<CourseDto?>;



internal class GetCourseBySlugQueryHandler(QueryContext db) : IBaseQueryHandler<GetCourseBySlugQuery, CourseDto?>
{
    public async Task<CourseDto?> Handle(GetCourseBySlugQuery request, CancellationToken cancellationToken)
    {
        var course = await db.Courses.Include(x => x.Teacher.User)
            .Include(x => x.Sections)
            .ThenInclude(sectionQueryModel => sectionQueryModel.Episodes)
            .Include(courseQueryModel => courseQueryModel.Category)
            .Include(courseQueryModel => courseQueryModel.SubCategory)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Slug == request.Slug, cancellationToken);


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
            Sections = course.Sections.Select(x => new CourseSectionDto
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                CourseId = x.CourseId,
                Title = x.Title,
                DisplayOrder = x.DisplayOrder,
                Episodes = x.Episodes.Select(e => new EpisodeDto
                {
                    Id = e.Id,
                    CreationDate = e.CreationDate,
                    SectionId = e.SectionId,
                    Title = e.Title,
                    EnglishTitle = e.EnglishTitle,
                    Token = e.Token,
                    TimeSpan = e.TimeSpan,
                    VideoName = e.VideoName,
                    AttachmentName = e.AttachmentName,
                    IsActive = e.IsActive,
                    IsFree = e.IsFree
                }).ToList()
            }).ToList(),
            Teacher = new TeacherDto
            {
                Id = course.Teacher.Id,
                CreationDate = course.Teacher.CreationDate,
                UserName = course.Teacher.UserName,
                Status = course.Teacher.Status,
                CvFileName = "",
                User = new CoreModuleUserDto
                {
                    Id = course.Teacher.User.Id,
                    CreationDate = course.Teacher.User.CreationDate,
                    Avatar = course.Teacher.User.Avatar,
                    Name = course.Teacher.User.Name,
                    Family = course.Teacher.User.Family,
                    Email = course.Teacher.User.Email,
                    PhoneNumber = course.Teacher.User.phoneNumber
                }
            },
            MainCategory = new CourseCategoryDto
            {
                Id = course.Category.Id,
                CreationDate = course.Category.CreationDate,
                Title = course.Category.Title,
                Slug = course.Category.Slug,
                ParentId = course.Category.ParentId,
                Children = null
            },
            SubCategory = new CourseCategoryDto
            {
                Id = course.SubCategory.Id,
                CreationDate = course.SubCategory.CreationDate,
                Title = course.SubCategory.Title,
                Slug = course.SubCategory.Slug,
                ParentId = course.SubCategory.ParentId,
                Children = null
            }
        };
    }
}



