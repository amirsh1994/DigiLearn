using Common.Application;
using Common.Application.SecurityUtil;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public Guid UserId { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
}


public class ChangePasswordCommandHandler(UserContext db) : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        var hashedPassword = user.password;
        if (Sha256Hasher.IsCompare(hashedPassword, request.CurrentPassword) == false)
        {
            return OperationResult.Error("پسورد جاری اشتباه هست");
        }
        var newHashedPassword = Sha256Hasher.Hash(request.NewPassword);
        user.password = newHashedPassword;
        await db.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}


public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .WithMessage("پسورد نم یتواند کوچکتر از 6 کاراکتر باشد");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .NotNull();
    }
}