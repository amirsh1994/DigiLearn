using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Notifications.DeleteAll;

public record DeleteAllNotificationCommand(Guid UserId):IBaseCommand;

    



public class DeleteAllNotificationCommandHandler(UserContext db):IBaseCommandHandler<DeleteAllNotificationCommand>
{
    public async Task<OperationResult> Handle(DeleteAllNotificationCommand request, CancellationToken cancellationToken)
    {
        var notifications = await db.Notifications.Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);

        if (notifications.Any())
        {
            db.Notifications.RemoveRange(notifications);
            await db.SaveChangesAsync(cancellationToken);
        }
        return OperationResult.Success();
    }
}