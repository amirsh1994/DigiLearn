using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Courses.Enums;

namespace CoreModule.Query._Data.Entities;

[Table("Courses",Schema = "course")]
internal class CourseQueryModel : BaseEntity
{
    public Guid TeacherId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid SubCategoryId { get; set; }

    public string Slug { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageName { get; set; }

    public string? VideoName { get; set; }

    public int Price { get; set; }

    public DateTime LastUpdate { get; set; }

    public CourseLevel CourseLevel { get; set; }

    public CourseStatus CourseStatus { get; set; }

    public SeoData SeoData { get; set; }

    public List<SectionQueryModel> Sections { get; } = [];

    [ForeignKey(nameof(TeacherId))]
    public TeacherQueryModel Teacher { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public CourseCategoryQueryModel Category { get; set; }

    [ForeignKey(nameof(SubCategoryId))]
    public CourseCategoryQueryModel SubCategory { get; set; }
}