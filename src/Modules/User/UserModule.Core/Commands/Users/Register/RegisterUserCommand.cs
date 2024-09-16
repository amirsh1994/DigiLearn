using System.Security.Cryptography;
using Common.Application;
using Common.Application.SecurityUtil;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;
using UserModule.Data.Entities.Users;

namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserCommand : IBaseCommand<Guid>
{
    public string PhoneNumber { get; set; }

    public string Password { get; set; }



}


internal class RegisterUserCommandHandler(UserContext db) : IBaseCommandHandler<RegisterUserCommand, Guid>
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