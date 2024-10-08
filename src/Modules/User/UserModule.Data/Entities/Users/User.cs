﻿using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace UserModule.Data.Entities.Users;

public class User : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [MaxLength(11)]
    [Required]
    public  string phoneNumber { get; set; }

    [MaxLength(70)]
    [Required]
    public string password { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(100)]
    [Required]
    public string Avatar { get; set; }

    public List<UserRole> UserRoles { get; set; } = [];
}