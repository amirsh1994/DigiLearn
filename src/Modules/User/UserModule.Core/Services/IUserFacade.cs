using Common.Application;
using MediatR;
using UserModule.Core.Commands.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Queries.Users.GetByPhoneNumber;

namespace UserModule.Core.Services;

public interface IUserFacade
{
    Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command);

    Task<OperationResult> EditUserProfile(EditUserCommand command);

    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);

    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);


}


public class UserFacade(IMediator mediator):IUserFacade
{
    public async Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> EditUserProfile(EditUserCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }
}