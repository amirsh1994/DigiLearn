using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace UserModule.Data.Entities.Roles;

internal class Role : BaseEntity
{
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
}