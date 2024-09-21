using AutoMapper;
using Common.Application;
using FluentValidation;
using UserModule.Data;
using UserModule.Data.Entities.Notifications;

namespace UserModule.Core.Commands.Notifications.Create;

public class CreateNotificationCommand : IBaseCommand
{
    public Guid UserId { get; set; }

    public string Text { get; set; }

    public string Title { get; set; }
}


public class CreateNotificationCommandHandler(UserContext db,IMapper mapper) : IBaseCommandHandler<CreateNotificationCommand>
{
    public async Task<OperationResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<UserNotification>(request);
        db.Notifications.Add(model);
        await db.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}


public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("title can not be empty or null");

        RuleFor(x => x.Text)
            .NotNull()
            .NotEmpty()
            .WithMessage("text can not be empty or null");
    }
}
