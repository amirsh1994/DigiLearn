using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teacher.AcceptRequest;

public class AcceptTeacherRequestCommand:IBaseCommand
{
    public Guid TeacherId { get; set; }
}


public class AcceptTeacherRequestCommandHandler(ITeacherRepository repository):IBaseCommandHandler<AcceptTeacherRequestCommand>
{
    public async Task<OperationResult> Handle(AcceptTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await repository.GetTracking(request.TeacherId);

        if (teacher==null)
        {
            return OperationResult.NotFound();
        }
        teacher.AcceptRequest();
        await repository.Save();
        return OperationResult.Success();
    }
}