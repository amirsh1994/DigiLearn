using Common.Domain;

namespace CoreModule.Infrastructure.Persistence.Users;

internal class User:BaseEntity
{
    public string Name { get; set; }

    public string Family { get; set; }

    public string? Email { get; set; }

    public string phoneNumber { get; set; }

    public string Avatar { get; set; }
}