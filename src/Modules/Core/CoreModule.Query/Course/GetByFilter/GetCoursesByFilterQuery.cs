using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.GetByFilter;

public class GetCoursesByFilterQuery(CourseFilterParams filterParams) : BaseQueryFilter<CourseFilterResult, CourseFilterParams>(filterParams)
{

}
internal class GetCourseByFilterQueryHandler(QueryContext db) : IBaseQueryHandler<GetCoursesByFilterQuery, CourseFilterResult>
{
    public async Task<CourseFilterResult> Handle(GetCoursesByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var q = db.Courses
            .Include(x=>x.Teacher.User)
            .Include(x => x.Sections)
            .ThenInclude(x => x.Episodes)
            .AsSplitQuery()
            .AsQueryable();

        q = @params.CourseFilterSort switch
        {
            CourseFilterSort.Latest => q.OrderByDescending(x => x.LastUpdate),
            CourseFilterSort.Oldest => q.OrderBy(x => x.LastUpdate),
            CourseFilterSort.Expensive => q.OrderByDescending(x => x.Price),
            CourseFilterSort.Cheapest => q.OrderBy(x => x.Price),
            _ => throw new ArgumentOutOfRangeException()
        };


        if (@params.TeacherId != null)
        {
            q = q.Where(x => x.TeacherId == @params.TeacherId);
        }


        if (@params.ActionStatus != null)
        {
            q = q.Where(x => x.Status == @params.ActionStatus);
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
                TeacherName =$"{x.Teacher.User.Name} {x.Teacher.User.Family}"  ,
                Price = x.Price,
                Sections = x.Sections.Select(s=>new CourseSectionDto
                {
                    Id = s.Id,
                    CreationDate = s.CreationDate,
                    CourseId = s.CourseId,
                    Title = s.Title,
                    DisplayOrder = s.DisplayOrder,
                    Episodes = s.Episodes.Select(e=>new EpisodeDto
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