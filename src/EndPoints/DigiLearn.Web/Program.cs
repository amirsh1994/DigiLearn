using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using CoreModule.Config;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.JwtUtil;
using TicketModule;
using UserModule.Core;

namespace DigiLearn.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        builder.Services.JwtAuthenticationConfig(builder.Configuration);
        builder.Services
            .InitUserModule(builder.Configuration)
            .InitTicketModule(builder.Configuration)
            .InitCoreModule(builder.Configuration);
        builder.Services.AddScoped<ILocalFileService, LocalFileService>();
        builder.Services.AddScoped<IFtpFileService, FtpFileService>();
        builder.Services.AddTransient<TeacherActionFilter>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Use((context, next) =>
        {
            var token = context.Request.Cookies["digi_Token"]?.ToString();
            if (string.IsNullOrWhiteSpace(token) == false)
            {
                context.Request.Headers.Append("Authorization", $"Bearer {token}");
            }
            return next();
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}

