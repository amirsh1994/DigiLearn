using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Category.GetById;

public record GetCourseCategoryById(Guid CategoryId) : IBaseQuery<CourseCategoryDto?>;





internal class GetCourseCategoryByIdHandler(QueryContext db):IBaseQueryHandler<GetCourseCategoryById, CourseCategoryDto?>
{
    public async Task<CourseCategoryDto?> Handle(GetCourseCategoryById request, CancellationToken cancellationToken)
    {
        var category = await db.Categories
            .Include(x => x.Children)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
        if (category == null)
        {
            return null;
        }
        return new CourseCategoryDto
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            ParentId = category.ParentId,
            Children = category.Children.Select(x => new CourseCategoryChild
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Title = x.Title,
                Slug = x.Slug,
                ParentId = x.ParentId
            }).ToList()
        };
    }
}