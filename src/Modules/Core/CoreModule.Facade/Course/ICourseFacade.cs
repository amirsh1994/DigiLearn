using Common.Application;
using CoreModule.Application.Course.Create;
using CoreModule.Application.Course.Edit;
using CoreModule.Application.Course.Episodes.Add;
using CoreModule.Application.Course.Sections.AddSection;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Course.GetByFilter;
using CoreModule.Query.Course.GetById;
using MediatR;

namespace CoreModule.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);

    Task<OperationResult> Edit(EditCourseCommand command);

    Task<OperationResult> AddSection(AddCourseSectionCommand command);

    Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command);

    Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams filterParams);

    Task<CourseDto?> GetCourseById(Guid courseId);
}


public class CourseFacade(IMediator mediator) : ICourseFacade
{
    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> AddSection(AddCourseSectionCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams filterParams)
    {
        return await mediator.Send(new GetCoursesByFilterQuery(filterParams));
    }

    public async Task<CourseDto?> GetCourseById(Guid courseId)
    {
        return await mediator.Send(new GetCourseByIdQuery(courseId));
    }
}