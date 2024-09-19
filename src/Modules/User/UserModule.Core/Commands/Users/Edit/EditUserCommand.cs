using Common.Application;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Family { get; set; }

    public string? Email { get; set; }


}


public class EditUserCommandHandler(UserContext db):IBaseCommandHandler<EditUserCommand>
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