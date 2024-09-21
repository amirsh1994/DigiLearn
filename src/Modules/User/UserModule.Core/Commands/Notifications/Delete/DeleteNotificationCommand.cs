using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;
using UserModule.Data.Entities.Users;

namespace UserModule.Core.Commands.Notifications.Delete;

public record DeleteNotificationCommand(Guid NotificationId,Guid UserId) : IBaseCommand;

    



public class DeleteNotificationCommandHandler(UserContext db):IBaseCommandHandler<DeleteNotificationCommand>
{
    public async Task<OperationResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = await db.Notifications.FirstOrDefaultAsync(x => x.Id == request.NotificationId && x.UserId==request.UserId,cancellationToken);

        if (model==null)
        {
            return OperationResult.NotFound();
        }
        db.Notifications.Remove(model);
        await db.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}