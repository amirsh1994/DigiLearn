using Common.Application;
using CoreModule.Application.Teacher.AcceptRequest;
using CoreModule.Application.Teacher.Register;
using CoreModule.Application.Teacher.RejectRequest;
using MediatR;

namespace CoreModule.Facade.Teacher;

public interface ITeacherFacade
{
    Task<OperationResult> Register(RegisterTeacherCommand command);

    Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command);

    Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command);
}



public class TeacherFacade(IMediator mediator) : ITeacherFacade
{
    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command)
    {
        return await mediator.Send(command);
    }
}