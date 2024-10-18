using System.Text;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Infrastructure.Persistence;
using CoreModule.Infrastructure.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreModule.Infrastructure.EventHandlers;

public class UserEditedEventHandler(ILogger<UserEditedEventHandler> logger, IEventBus eventBus, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private const string QueName = "coreModuleUserEdited";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(4), stoppingToken);
        var serviceScope = serviceScopeFactory.CreateScope();
        var db = serviceScope.ServiceProvider.GetRequiredService<CoreModuleEfContext>();
        var connection = eventBus.Connection;
        var model = connection.CreateModel();
        model.ExchangeDeclare(Exchanges.UserTopicExchange, ExchangeType.Topic, true, false, null);
        model.QueueDeclare(QueName, true, false, false, null);
        model.QueueBind(QueName, Exchanges.UserTopicExchange, "user.edited", null);
        var consumer = new EventingBasicConsumer(model);
        consumer.Received += async (sender, args) =>
        {
            try
            {
                var jsonResult = Encoding.UTF8.GetString(args.Body.ToArray());
                var edited = JsonConvert.DeserializeObject<UserEdited>(jsonResult);
                var oldUser = await db.Users.FirstOrDefaultAsync(x => x.Id == edited!.UserId, stoppingToken);
                if (oldUser == null)
                {
                    db.Users.Add(new User
                    {
                        Id = edited.UserId,
                        CreationDate = edited.CreationDate,
                        Name = edited.Name,
                        Family = edited.Family,
                        Email = edited.Email,
                        phoneNumber = edited.PhoneNumber,
                        Avatar = "default.png"
                    });
                }
                else
                {
                    oldUser.Name = edited.Name;
                    oldUser.Family = edited.Family;
                    oldUser.Email= edited.Email;
                    oldUser.phoneNumber= edited.PhoneNumber;
                    oldUser.CreationDate=edited.CreationDate;
                    db.Update(oldUser);
                }
                await db.SaveChangesAsync(stoppingToken);
                logger.LogWarning("User Updated in coreModule successfully");
                model.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        };
        model.BasicConsume(QueName, false, consumer);
    }
}