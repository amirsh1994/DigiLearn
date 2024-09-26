using CoreModule.Query._Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query._Data;

internal class QueryContext(DbContextOptions<QueryContext> options) : DbContext(options)
{
    public DbSet<UserQueryModel> Users { get; set; }

    public DbSet<TeacherQueryModel> Teachers { get; set; }

    public DbSet<CourseQueryModel> Courses { get; set; }

    public DbSet<SectionQueryModel> Sections { get; set; }

    public DbSet<EpisodeQueryModel> Episodes { get; set; }

    public DbSet<CourseCategoryQueryModel> Categories { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override int SaveChanges()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<TeacherQueryModel>(builder =>
        {
            builder.Property(x => x.UserName)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            builder.ToTable("Teachers");
        });

        modelBuilder.Entity<CourseQueryModel>(builder =>
        {
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
        });
        modelBuilder.Entity<EpisodeQueryModel>(builder =>
        {

        });
        modelBuilder.Entity<CourseCategoryQueryModel>(builder =>
        {

        });
        base.OnModelCreating(modelBuilder);
    }
}