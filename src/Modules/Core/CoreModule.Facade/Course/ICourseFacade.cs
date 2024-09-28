using Common.Application;
using CoreModule.Application.Course.Create;
using CoreModule.Application.Course.Edit;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Course.GetByFilter;
using MediatR;

namespace CoreModule.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);

    Task<OperationResult> Edit(EditCourseCommand command);

    Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams filterParams);
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

    public async Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams filterParams)
    {
        return await mediator.Send(new GetCoursesByFilterQuery(filterParams));
    }
}