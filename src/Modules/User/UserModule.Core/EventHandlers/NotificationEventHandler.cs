using System.Text;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Services;

namespace UserModule.Core.EventHandlers;

public class NotificationEventHandler(IEventBus eventBus, ILogger<NotificationEventHandler> logger, IServiceScopeFactory serviceScopeFactory):BackgroundService
{
    private const string QueName = "userNotificationHandler";
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        var serviceScope = serviceScopeFactory.CreateScope();
        var notificationFacade = serviceScope.ServiceProvider.GetRequiredService<INotificationFacade>();
        var connection = eventBus.Connection;
        var model = connection.CreateModel();
        model.ExchangeDeclare(Exchanges.NotificationExchange, ExchangeType.Fanout, true, false, null);
        model.QueueDeclare(QueName, true, false, false, null);
        model.QueueBind(QueName, Exchanges.NotificationExchange, "", null);
        var consumer = new EventingBasicConsumer(model);
        consumer.Received += async (sender, args) =>
        {
            try
            {
                var jsonResult = Encoding.UTF8.GetString(args.Body.ToArray());
                var notification = JsonConvert.DeserializeObject<NewNotificationIntegrationEvent>(jsonResult);
                await notificationFacade.CreateNotification(new CreateNotificationCommand
                {
                    UserId = notification!.UserId,
                    Text = notification.Message,
                    Title = notification.Title
                });
                logger.LogWarning("added successfully to userNotification....");
                model.BasicAck(args.DeliveryTag,false);
            }
            catch (Exception e)
            {

                logger.LogError(e,e.Message);
            }

        };


        model.BasicConsume(consumer, "");

    }
}