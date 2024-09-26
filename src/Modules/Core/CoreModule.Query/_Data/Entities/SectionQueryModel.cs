using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;

namespace CoreModule.Query._Data.Entities;

[Table("Sections", Schema = "course")]
internal class SectionQueryModel : BaseEntity
{
    public Guid CourseId { get;  set; }

    public string Title { get; set; }

    public List<EpisodeQueryModel> Episodes { get;set; } = [];

    public int DisplayOrder { get;  set; }

    [ForeignKey(nameof(CourseId))]
    public CourseQueryModel Course { get; set; }
}