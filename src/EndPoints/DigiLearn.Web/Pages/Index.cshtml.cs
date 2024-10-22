using DigiLearn.Web.Infrastructure.Services;
using DigiLearn.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class IndexModel(IHomePageService homePageService) : PageModel
{

    public HomePageViewModel HomePageData { get; set; }

    public async Task OnGet()
    {
        HomePageData = await homePageService.GetData();
    }
}

