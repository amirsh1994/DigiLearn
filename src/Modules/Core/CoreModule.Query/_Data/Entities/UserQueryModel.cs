using Common.Domain;
using CoreModule.Domain.Courses.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("users", Schema = "dbo")]
internal class UserQueryModel : BaseEntity
{

    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Family { get; set; }
    [MaxLength(110)]
    public string? Email { get; set; }

    [MaxLength(12)]
    public string phoneNumber { get; set; }
    [MaxLength(110)]
    public string Avatar { get; set; }
}