using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Notifications.Seen;

public record SeenNotificationCommand(Guid NotificationId):IBaseCommand;

public class SeenNotificationCommandHandler(UserContext db):IBaseCommandHandler<SeenNotificationCommand>
{
    public async Task<OperationResult> Handle(SeenNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await db.Notifications.FirstOrDefaultAsync(x => x.Id == request.NotificationId && x.IsSeen==false,cancellationToken);
        if (notification == null)
        {
            return OperationResult.NotFound();
        }
        notification.IsSeen = true;
        db.Update(notification);
        await db.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}