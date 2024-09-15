using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Context;

internal class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
{

    public DbSet<Category> Categories { get; set; }

    public DbSet<Post> Posts { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}