using Common.Infrastructure;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Teachers.Models;
using CoreModule.Infrastructure.Persistence.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistence;

public class CoreModuleEfContext(DbContextOptions<CoreModuleEfContext> options, IMediator mediator) : BaseEfContext<CoreModuleEfContext>(options, mediator)
{
    public DbSet<Domain.Courses.Models.Course> Courses { get; set; }

    public DbSet<Domain.Teachers.Models.Teacher> Teachers { get; set; }

    public DbSet<CourseCategory> Categories { get; set; }

    private DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        modelBuilder.HasDefaultSchema("dbo");
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}