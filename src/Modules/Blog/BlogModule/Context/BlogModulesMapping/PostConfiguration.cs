using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogModule.Context.BlogModulesMapping;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique(true);
        builder.Property(x => x.Title).HasMaxLength(80);
        builder.Property(x => x.Slug).HasMaxLength(80);
        builder.Property(x => x.OwnerName).HasMaxLength(80);
        builder.Property(x => x.Description).HasMaxLength(4000);
        builder.Property(x => x.ImageName).HasMaxLength(400);
        builder.ToTable("Posts", "dbo");


    }
}

internal class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(80);
        builder.Property(x => x.slug).HasMaxLength(80);

    }
}