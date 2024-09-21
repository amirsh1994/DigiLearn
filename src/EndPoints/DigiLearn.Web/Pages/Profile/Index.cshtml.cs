using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

public class IndexModel(IUserFacade userFacade, INotificationFacade notificationFacade):PageModel
{
    public UserDto UserDto { get; set; }

    public List<NotificationFilterData> NewNotifications { get; set; }

    public async Task OnGet()
    {
        var user = await userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        UserDto = user;

        var result =await notificationFacade.GetNotificationsByFilter(new NotificationFilterParams
        {
            PageId = 1,
            Take = 5,
            UserId = UserDto!.Id,
            IsSeen = false
        });
        NewNotifications = result.Data;
    }
}

