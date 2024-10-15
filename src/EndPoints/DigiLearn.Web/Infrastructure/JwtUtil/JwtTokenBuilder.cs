using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UserModule.Core.Queries._DTOs;

namespace DigiLearn.Web.Infrastructure.JwtUtil;

public class JwtTokenBuilder
{
    public static string BuildToken(UserDto user, IConfiguration configuration)
    {
        var roles = user.Roles.Select(x => x.Title);
        var claims = new List<Claim>()
        {

            new(ClaimTypes.MobilePhone,user.phoneNumber),
            new (ClaimTypes.NameIdentifier,user.Id.ToString()),
            new (ClaimTypes.Role,string.Join("-",roles)),
        };
        var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"], 
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials:credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 