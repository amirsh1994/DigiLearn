using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;

namespace CoreModule.Query._Data.Entities;

[Table("Categories")]
internal class CourseCategoryQueryModel:BaseEntity
{
    public string Title { get;  set; }

    public string Slug { get; set; }

    public Guid? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public List<CourseCategoryQueryModel> Children { get; set; }
}