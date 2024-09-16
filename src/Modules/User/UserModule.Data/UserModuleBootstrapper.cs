using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserModule.Data;

public static class UserModuleBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext<UserContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("User_Context"));
        });

        return service;
    }
}