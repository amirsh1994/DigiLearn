using Common.Domain;

namespace UserModule.Data.Entities.Roles;

internal class RolePermission:BaseEntity
{
    public Guid RoleId { get; set; }

    public RolePermission Permission { get; set; }

    public Role Role { get; set; }
}