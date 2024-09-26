using Common.Application;
using CoreModule.Application.Course.Create;
using CoreModule.Application.Course.Edit;
using MediatR;

namespace CoreModule.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);

    Task<OperationResult> Edit(EditCourseCommand command);


}


public class CourseFacade(IMediator  mediator):ICourseFacade
{
    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await mediator.Send(command);
    }
}