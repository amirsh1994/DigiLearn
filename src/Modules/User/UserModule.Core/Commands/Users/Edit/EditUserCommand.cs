using Common.Application;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Family { get; set; }

    public string? Email { get; set; }
}


public class EditUserCommandHandler(UserContext db,IEventBus eventBus):IBaseCommandHandler<EditUserCommand>
{
    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        user.Name = request.Name;
        user.Family = request.Family;
        if (string.IsNullOrWhiteSpace(request.Email)==false)
        {
            user.Email = request.Email;
        }
        db.Update(user);
        await db.SaveChangesAsync(cancellationToken);
        eventBus.Publish(new UserEdited
        {
            UserId = user.Id,
            Name = user.Name,
            Family = user.Family,
            Email = user.Email,
            PhoneNumber = user.phoneNumber
        },null,Exchanges.UserTopicExchange,ExchangeType.Topic,"user.edited");
        return OperationResult.Success();
    }
}


public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name cant not be empty or null from userModule Core command");

        RuleFor(x => x.Family)
            .NotEmpty()
            .NotNull()
            .WithMessage("Family cant not be empty or null from userModule Core command");
    }
}