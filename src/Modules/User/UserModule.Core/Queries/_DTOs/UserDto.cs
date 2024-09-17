namespace UserModule.Core.Queries._DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Name { get; set; }

    public string? Family { get; set; }

    public string phoneNumber { get; set; }

    public string password { get; set; }

    public string? Email { get; set; }

    public string Avatar { get; set; }

    public List<RoleDto> Roles { get; set; } = [];

}

public class RoleDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

}
