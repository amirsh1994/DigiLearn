using CoreModule.Domain.Teachers.Events;
using MediatR;

namespace CoreModule.Application._EventHandlers;

internal class AcceptRequestEventHandler : INotificationHandler<AcceptRequestDomainEvent>
{
    public async Task Handle(AcceptRequestDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}


internal class RejectRequestEventHandler : INotificationHandler<RejectRequestDomainEvent>
{
    public async Task Handle(RejectRequestDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}


