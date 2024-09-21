using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.Notifications.Delete;
using UserModule.Core.Commands.Notifications.DeleteAll;
using UserModule.Core.Commands.Notifications.Seen;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

public class NotificationsModel(INotificationFacade notificationFacade) : BaseRazorFilter<NotificationFilterParams>
{
    public NotificationFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        var result = await notificationFacade.GetNotificationsByFilter(new NotificationFilterParams
        {
            PageId = FilterParams.PageId,
            Take = 6,
            UserId = User.GetUserId(),
            IsSeen = null
        });
        FilterResult = result;
    }

    public async Task<IActionResult> OnPostDeleteAll()
    {
        return await AjaxTryCatch(async () =>
        {
            return await notificationFacade.DeleteAllNotifications(new DeleteAllNotificationCommand(User.GetUserId()));
        });
    }

    public async Task<IActionResult> OnPostDeleteNotification(Guid notificationId)
    {
        return await AjaxTryCatch(async () =>
        {
            return await notificationFacade.DeleteNotification(new DeleteNotificationCommand(notificationId,User.GetUserId()));
        });
    }

    public async Task<IActionResult> OnPostSeenNotification(Guid notificationId)
    {
        var result =await notificationFacade.SeenNotification(new SeenNotificationCommand(notificationId));
        return RedirectAndShowAlert(result, RedirectToPage("Notifications"));
    }
}

