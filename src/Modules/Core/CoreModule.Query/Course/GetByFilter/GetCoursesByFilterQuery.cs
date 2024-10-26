using Common.Query;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Query._Data;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.GetByFilter;

public class GetCoursesByFilterQuery(CourseFilterParams filterParams):BaseQueryFilter<CourseFilterResult, CourseFilterParams>(filterParams)
{

}
internal class GetCourseByFilterQueryHandler(QueryContext db):IBaseQueryHandler<GetCoursesByFilterQuery, CourseFilterResult>
{
    public async Task<CourseFilterResult> Handle(GetCoursesByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var q = db.Courses
            .Include(x => x.Teacher.User)
            .Include(x => x.Sections)
            .ThenInclude(x => x.Episodes)
            .Include(x => x.Category)
            .Include(x => x.SubCategory)
            .AsSplitQuery()
            .AsQueryable();

        q = @params.FilterSort switch
        {
            CourseFilterSort.Latest => q.OrderByDescending(x => x.LastUpdate),
            CourseFilterSort.Oldest => q.OrderBy(x => x.LastUpdate),
            CourseFilterSort.Expensive => q.OrderByDescending(x => x.Price),
            CourseFilterSort.Cheapest => q.OrderBy(x => x.Price),
            _ => throw new ArgumentOutOfRangeException()
        };

        switch (request.FilterParams.SearchByPrice)
        {
            case SearchByPrice.Free:
                q = q.Where(r => r.Price == 0);
                break;
            case SearchByPrice.NotFree:
                q = q.Where(r => r.Price > 0);
                break;
        }

        if (@params.CourseStatus!=null)
        {
            q = q.Where(x => x.CourseStatus == @params.CourseStatus);
        }

        if (@params.CourseLevel!=null)
        {
            q = q.Where(x => x.CourseLevel == @params.CourseLevel);
        }


        if (@params.TeacherId != null)
        {
            q = q.Where(x => x.TeacherId == @params.TeacherId);
        }

        if (@params.ActionStatus != null)
        {
            q = q.Where(x => x.Status == @params.ActionStatus);
        }

        if (string.IsNullOrWhiteSpace(@params.CategorySlug) == false)
        {
            q = q.Where(x => x.Category.Slug == @params.CategorySlug || x.SubCategory.Slug == @params.CategorySlug);
        }

        if (string.IsNullOrWhiteSpace(@params.Search)==false)
        {
            q = q.Where(x => x.Title.Contains(@params.Search) || x.Slug.Contains(@params.Search));
        }

        var skip = (@params.PageId - 1) * @params.Take;
        var take = @params.Take;
        var data = await q.Skip(skip).Take(take).ToListAsync(cancellationToken);
        var model = new CourseFilterResult()
        {
            Data = data.Select(x => new CourseFilterData
            {
                Id = x.Id,
                CourseStatus = x.Status,
                CreationDate = x.CreationDate,
                ImageName = x.ImageName,
                Title = x.Title,
                Slug = x.Slug,
                TeacherName = $"{x.Teacher.User.Name} {x.Teacher.User.Family}",
                Price = x.Price,
                Sections = x.Sections.Select(s => new CourseSectionDto
                {
                    Id = s.Id,
                    CreationDate = s.CreationDate,
                    CourseId = s.CourseId,
                    Title = s.Title,
                    DisplayOrder = s.DisplayOrder,
                    Episodes = s.Episodes.Select(e => new EpisodeDto
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
                }).ToList()
            }).ToList()
        };
        model.GeneratePaging(q, take, @params.PageId);
        return model;
    }
}