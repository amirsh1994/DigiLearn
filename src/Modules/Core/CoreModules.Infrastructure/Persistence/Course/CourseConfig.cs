using Common.Infrastructure.Repository;
using CoreModule.Domain.Courses.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistence.Course;

public class CourseConfig : IEntityTypeConfiguration<Domain.Courses.Models.Course>
{
    public void Configure(EntityTypeBuilder<Domain.Courses.Models.Course> builder)
    {
        builder.ToTable("Courses", "course");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.ImageName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.VideoName)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.OwnsOne(x => x.SeoData, config =>
        {
            config.Property(x => x.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            config.Property(x => x.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

            config.Property(x => x.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

            config.Property(x => x.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");
        });

        builder.OwnsMany(x => x.Sections, config =>
        {
            config.ToTable("Sections", "course");

            config.HasKey(x => x.Id);

            config.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            config.OwnsMany(b => b.Episodes, option =>
            {
                option.HasKey(x => x.Id);

                option.ToTable("Episodes", "course");

                option.Property(x => x.Title)
                    .HasMaxLength(100);

                option.Property(x => x.EnglishTitle)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(100);

                option.Property(x=>x.VideoName)
                    .IsRequired()
                    .HasMaxLength(200);

                option.Property(x => x.AttachmentName)
                    .IsRequired(false)
                    .HasMaxLength(200);
            });
        });
    }
}

public class CourseRepository(CoreModuleEfContext context):BaseRepository<Domain.Courses.Models.Course, CoreModuleEfContext>(context), ICourseRepository;