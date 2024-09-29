using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Category.GetChildren;

public record GetCourseCategoryChildrenQuery(Guid ParentId) : IBaseQuery<List<CourseCategoryDto>>;





internal class GetCourseCategoryChildrenQueryHandler(QueryContext db) : IBaseQueryHandler<GetCourseCategoryChildrenQuery, List<CourseCategoryDto>>
{
    public async Task<List<CourseCategoryDto>> Handle(GetCourseCategoryChildrenQuery request, CancellationToken cancellationToken)
    {
        return await db.Categories
            .Where(x => x.ParentId == request.ParentId)
            .OrderByDescending(x => x.CreationDate)
            .Select(x => new CourseCategoryDto
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Title = x.Title,
                Slug = x.Slug,
                ParentId = x.ParentId,
            }).ToListAsync(cancellationToken);
    }
}