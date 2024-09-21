using Common.Application;
using MediatR;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Commands.Notifications.Delete;
using UserModule.Core.Commands.Notifications.DeleteAll;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Queries.Notifications.GetFilter;

namespace UserModule.Core.Services;

public interface INotificationFacade
{
    Task<OperationResult> CreateNotification(CreateNotificationCommand command);

    Task<OperationResult> DeleteNotification(DeleteNotificationCommand command);

    Task<OperationResult> DeleteAllNotifications(DeleteAllNotificationCommand command);

    Task<NotificationFilterResult> GetNotificationsByFilter(NotificationFilterParams @params);
}


public class NotificationFacade(IMediator mediator) : INotificationFacade
{
    public async Task<OperationResult> CreateNotification(CreateNotificationCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> DeleteNotification(DeleteNotificationCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> DeleteAllNotifications(DeleteAllNotificationCommand command)
    {
        return await mediator.Send(command);
    }


    public async Task<NotificationFilterResult> GetNotificationsByFilter(NotificationFilterParams @params)
    {
        return await mediator.Send(new GetNotificationsByFilterQuery(@params));
    }
}