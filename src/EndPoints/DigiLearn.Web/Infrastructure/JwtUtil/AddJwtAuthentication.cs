using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DigiLearn.Web.Infrastructure.JwtUtil;

public static class AddJwtAuthentication
{

    public static void JwtAuthenticationConfig(this IServiceCollection service, IConfiguration configuration)
    {
        //logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
        service.AddAuthentication(option =>
        {
            option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"])),
                ValidIssuer = configuration["JwtConfig:Issuer"],
                ValidAudience = configuration["JwtConfig:Audience"],
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
            };
            option.SaveToken = true;//اگه این برابر با ترو باشه میتونیم توکن یوزر رو از httpcontext  دریافتش کنیم
        });
    }

}
