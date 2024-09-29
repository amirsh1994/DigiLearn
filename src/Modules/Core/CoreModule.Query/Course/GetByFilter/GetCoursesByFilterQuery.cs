using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.GetByFilter;

public class GetCoursesByFilterQuery(CourseFilterParams filterParams):BaseQueryFilter<CourseFilterResult, CourseFilterParams>(filterParams)
{

}


internal class GetCourseByFilterQueryHandler(QueryContext db) : IBaseQueryHandler<GetCoursesByFilterQuery, CourseFilterResult>
{
    public async Task<CourseFilterResult> Handle(GetCoursesByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var q = db.Courses
            .Include(x=>x.Sections)
            .ThenInclude(x=>x.Episodes)
            .AsSplitQuery()
            .OrderByDescending(x=>x.LastUpdate).AsQueryable();

        if (@params.TeacherId!=null)
        {
            q = q.Where(x => x.TeacherId == @params.TeacherId);
        }
        var skip = (@params.PageId - 1)*@params.Take;
        var take = @params.Take;
        var data = await q.Skip(skip).Take(take).ToListAsync(cancellationToken);
        var model = new CourseFilterResult()
        {
            Data =data.Select(x=>new CourseFilterData
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                ImageName = x.ImageName,
                Title = x.Title,
                Slug = x.Slug,
                Price = x.Price,
                EpisodeCount=x.Sections.Sum(e=>e.Episodes.Count())
            }).ToList()
        };
        model.GeneratePaging(q,take,@params.PageId);
        return model;
    }
}