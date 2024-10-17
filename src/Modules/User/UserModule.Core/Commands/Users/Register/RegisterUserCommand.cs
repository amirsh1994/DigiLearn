using System.Security.Cryptography;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using UserModule.Data;
using UserModule.Data.Entities.Users;

namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserCommand : IBaseCommand<Guid>
{
    public string PhoneNumber { get; set; }

    public string Password { get; set; }

}


internal class RegisterUserCommandHandler(UserContext db,IEventBus eventBus) : IBaseCommandHandler<RegisterUserCommand, Guid>
{

    public async Task<OperationResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await db.Users.AnyAsync(x => x.phoneNumber == request.PhoneNumber, cancellationToken))
        {
            return OperationResult<Guid>.Error("شما قبلا با این شماره تلفن ثبت نام کرده اید..!");
        }

        var user = new User()
        {
            password = Sha256Hasher.Hash(request.Password)
            ,
            phoneNumber = request.PhoneNumber
            ,
            Avatar = "default.png"
            ,
            Id = Guid.NewGuid()
        };
        db.Users.Add(user);
        await db.SaveChangesAsync(cancellationToken);
        eventBus.Publish(new UserRegistered
        {
            Id = user.Id,
            Name = user.Name,
            Family = user.Family,
            PhoneNumber = user.phoneNumber,
            Email = user.Email,
            Password = user.password,
            Avatar = user.Avatar
        },null,Exchanges.UserTopicExchange,ExchangeType.Topic,"user.registered");
        return OperationResult<Guid>.Success(user.Id);
    }
}


public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}