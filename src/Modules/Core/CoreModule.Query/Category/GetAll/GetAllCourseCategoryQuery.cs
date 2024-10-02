using AngleSharp.Common;
using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Category.GetAll;

public class GetAllCourseCategoryQuery : IBaseQuery<List<CourseCategoryDto>>
{

}


internal class GetAllCourseCategoryQueryHandler(QueryContext db) : IBaseQueryHandler<GetAllCourseCategoryQuery, List<CourseCategoryDto>>
{
    public async Task<List<CourseCategoryDto>> Handle(GetAllCourseCategoryQuery request, CancellationToken cancellationToken)
    {
        return await db.Categories
            .Where(x=>x.ParentId==null)
            .OrderByDescending(x=>x.CreationDate)
            .Include(x=>x.Children)
            .AsSplitQuery()
            .Select(x => new CourseCategoryDto
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Title = x.Title,
                Slug = x.Slug,
                ParentId = x.ParentId,
                Children =x.Children.Select(c=>new CourseCategoryChild
                {
                    Id = c.Id,
                    CreationDate = c.CreationDate,
                    Title = c.Title,
                    Slug = c.Slug,
                    ParentId = c.ParentId
                }).ToList()
            }).ToListAsync(cancellationToken);
    }
}