using CoreModule.Query._Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Query;

public static class CoreModuleQueryBootstrapper
{
    public static void RegisterDependency(IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext<QueryContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoreModule_Context"));
        });
        service.AddMediatR(typeof(QueryContext).Assembly);
    }
}