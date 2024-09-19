﻿using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Infrastructure;

public static class RegisterDependencyServices
{
    public static IServiceCollection RegisterWebDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        //services.AddSingleton<IEventBus, EventBusRabbitMQ>();
        services.AddTransient<IRenderViewToString, RenderViewToString>();
        services.AddAutoMapper(typeof(RegisterDependencyServices).Assembly);
        //services.AddScoped<IHomePageService, HomePageService>();
        return services;
    }
}