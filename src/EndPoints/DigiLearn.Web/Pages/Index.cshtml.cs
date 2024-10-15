using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class IndexModel(ILogger<IndexModel> logger, IEventBus eventBus) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public void OnGet()
    {
        eventBus.Publish(new UserRegistered(){Email = "test@test.com",Name = "Amir",PhoneNumber = "0935"},"test","digitLearn");


    }
}

