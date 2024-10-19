using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Teachers.Events;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.Teacher._EventHandler;

public class AcceptRequestEventHandler(IEventBus eventBus):INotificationHandler<AcceptRequestDomainEvent>
{
    public  Task Handle(AcceptRequestDomainEvent notification, CancellationToken cancellationToken)
    {
       eventBus.Publish(new NewNotificationIntegrationEvent
       {
           UserId = notification.UserId,
           Title = "تایید درخواست",
           Message = "تبریک پنل مدرسی شما  در این لینک در دسترس هست :<hr/><a href='/profile/teachers/courses'>ورود</a>",
           CreationDate = notification.CreationDate
       },"",Exchanges.NotificationExchange,ExchangeType.Fanout);
       return Task.CompletedTask;
    }
}


public class RejectRequestEventHandler(IEventBus eventBus):INotificationHandler<RejectRequestDomainEvent>
{
    public  Task Handle(RejectRequestDomainEvent notification, CancellationToken cancellationToken)
    {
        eventBus.Publish(new NewNotificationIntegrationEvent
        {
            UserId = notification.UserId,
            Title = "رد درخواست مدرسی",
            Message = $"کاربر گرامی درخواست مدرسی شما به دلیل زیر رد شد :<hr/><a>${notification.Description}<a/>",
            CreationDate = notification.CreationDate
        },"",Exchanges.NotificationExchange,ExchangeType.Fanout);
        return Task.CompletedTask;
    }

}