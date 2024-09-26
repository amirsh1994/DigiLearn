using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;
using CoreModule.Domain.Teachers.Enums;

namespace CoreModule.Query._Data.Entities;

internal class TeacherQueryModel : BaseEntity
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public TeacherStatus Status { get; set; }

    public string CvFileName { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserQueryModel User { get; set; }
}