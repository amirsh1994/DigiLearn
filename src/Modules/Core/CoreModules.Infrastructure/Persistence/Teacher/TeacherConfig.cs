using CoreModule.Infrastructure.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistence.Teacher;

public class TeacherConfig:IEntityTypeConfiguration<Domain.Teachers.Models.Teacher>
{
    public void Configure(EntityTypeBuilder<Domain.Teachers.Models.Teacher> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserName).IsUnique();

        builder.ToTable("Teachers");

        builder.Property(x => x.UserName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(20);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Domain.Teachers.Models.Teacher>(x => x.UserId);
    }
}