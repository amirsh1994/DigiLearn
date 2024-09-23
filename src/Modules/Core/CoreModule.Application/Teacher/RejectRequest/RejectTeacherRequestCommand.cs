using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teacher.RejectRequest;

public class RejectTeacherRequestCommand : IBaseCommand
{
    public Guid TeacherId { get; set; }

    public string Description { get; set; }

}


public class RejectTeacherRequestCommandHandler(ITeacherRepository repository) : IBaseCommandHandler<RejectTeacherRequestCommand>
{
    public async Task<OperationResult> Handle(RejectTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await repository.GetTracking(request.TeacherId);
        if (teacher == null)
        {
            return OperationResult.NotFound();
        }
        repository.Delete(teacher);
        //Todo Send Event
        await repository.Save();
        return OperationResult.Success();
    }
}