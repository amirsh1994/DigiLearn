using Common.Application;
using CoreModule.Application.Teacher.AcceptRequest;
using CoreModule.Application.Teacher.Register;
using CoreModule.Application.Teacher.RejectRequest;
using CoreModule.Query.Teacher._DTOs;
using CoreModule.Query.Teacher.GetById;
using CoreModule.Query.Teacher.GetByUserId;
using CoreModule.Query.Teacher.GetList;
using MediatR;

namespace CoreModule.Facade.Teacher;

public interface ITeacherFacade
{
    Task<OperationResult> Register(RegisterTeacherCommand command);

    Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command);

    Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command);

    Task<TeacherDto?> GetTeacherById(Guid id);

    Task<TeacherDto?> GetTeacherByUserId(Guid userId);

    Task<List<TeacherDto>> GetTeacherList();
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

    public async Task<TeacherDto?> GetTeacherById(Guid id)
    {
        return await mediator.Send(new GetTeacherByIdQuery(id));
    }

    public async Task<TeacherDto?> GetTeacherByUserId(Guid userId)
    {
        return await mediator.Send(new GetTeacherByUserIdQuery(userId));
    }

    public async Task<List<TeacherDto>> GetTeacherList()
    {
        return await mediator.Send(new GetTeacherListQuery());
    }
}