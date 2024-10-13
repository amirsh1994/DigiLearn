using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teacher.ToggleStatus;

public record ToggleTeacherStatusCommand(Guid TeacherId) : IBaseCommand;

    



public class ToggleTeacherStatusCommandHandler(ITeacherRepository repository):IBaseCommandHandler<ToggleTeacherStatusCommand>
{
    public async Task<OperationResult> Handle(ToggleTeacherStatusCommand request,CancellationToken cancellationToken)
    {
        var teacher = await repository.GetTracking(request.TeacherId);
        if (teacher == null)
        {
            return OperationResult.NotFound();
        }
        teacher.ToggleStatus();
        await repository.Save();
        return OperationResult.Success();
    }
}