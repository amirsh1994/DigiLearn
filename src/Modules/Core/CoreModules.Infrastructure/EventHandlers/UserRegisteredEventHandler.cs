using System.Text;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Infrastructure.Persistence;
using CoreModule.Infrastructure.Persistence.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreModule.Infrastructure.EventHandlers;

public class UserRegisteredEventHandler(IEventBus eventBus, IServiceScopeFactory serviceScopeFactory, ILogger<UserRegisteredEventHandler> logger) : BackgroundService
{
    private const string QueName = "coreModuleUserRegistered";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(4), stoppingToken);
        var scope = serviceScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CoreModuleEfContext>();
        var connection = eventBus.Connection;
        var model = connection.CreateModel();
        model.ExchangeDeclare(Exchanges.UserTopicExchange, ExchangeType.Topic, true, false, null);
        model.QueueDeclare(QueName, true, false, false, null);
        model.QueueBind(QueName, Exchanges.UserTopicExchange, "user.registered", null);
        var consumer = new EventingBasicConsumer(model);
        consumer.Received += async (sender, args) =>
        {
            try
            {
                var result = Encoding.UTF8.GetString(args.Body.ToArray());
                var u = JsonConvert.DeserializeObject<UserRegistered>(result);
                db.Users.Add(new User
                {
                    Id = u.Id,
                    CreationDate = u.CreationDate,
                    Name = u.Name,
                    Family = u.Family,
                    Email = u.Email,
                    phoneNumber = u.PhoneNumber,
                    Avatar = u.Avatar
                });
                await db.SaveChangesAsync(stoppingToken);
                logger.LogWarning("User Registered in coreModule Successfully.....!==>ooooowwwww");
                model.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        };
        model.BasicConsume(consumer, QueName, false);
    }
}