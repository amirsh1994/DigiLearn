using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace CoreModule.Infrastructure.Persistence.Users;

public class User : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }
    [MaxLength(50)]
    public string? Family { get; set; }
    [MaxLength(110)]
    public string? Email { get; set; }

    [MaxLength(12)]
    public string phoneNumber { get; set; }
    [MaxLength(110)]
    public string? Avatar { get; set; }
}