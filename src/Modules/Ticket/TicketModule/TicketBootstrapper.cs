using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketModule.Core.Services;
using TicketModule.Data;

namespace TicketModule;

public static class TicketBootstrapper
{
    public static IServiceCollection InitTicketModule(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddDbContext<TicketContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("Ticket_Context"));
        });
        service.AddAutoMapper(typeof(MapperProfile).Assembly);

        service.AddScoped<ITicketService, TicketService>();

        return service;
    }
}