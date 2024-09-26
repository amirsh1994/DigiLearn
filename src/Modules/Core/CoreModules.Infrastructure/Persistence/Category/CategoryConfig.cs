using Common.Infrastructure.Repository;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistence.Category;

public class CategoryConfig : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.Property(x => x.Slug)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);
    }
}

public class CategoryRepository(CoreModuleEfContext context):BaseRepository<CourseCategory, CoreModuleEfContext>(context),ICourseCategoryRepository
{
    public async Task Delete(CourseCategory category)
    {
        if (await context.Courses.AnyAsync(x => x.CategoryId == category.Id || x.SubCategoryId == category.Id))
        {
            throw new Exception("این دسته بندی دارای چندین دوره می باشد ...");
        }

        //ToDo Should Remove Children

        context.Remove(category);
        await context.SaveChangesAsync();
    }
}