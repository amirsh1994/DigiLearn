using Common.Application;
using CoreModule.Application.Course.Create;
using CoreModule.Application.Course.Edit;
using CoreModule.Application.Course.Episodes.Accept;
using CoreModule.Application.Course.Episodes.Add;
using CoreModule.Application.Course.Episodes.Delete;
using CoreModule.Application.Course.Episodes.Edit;
using CoreModule.Application.Course.Sections.AddSection;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Course.Episodes.GetById;
using CoreModule.Query.Course.GetByFilter;
using CoreModule.Query.Course.GetById;
using CoreModule.Query.Course.GetBySlug;
using MediatR;

namespace CoreModule.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);

    Task<OperationResult> Edit(EditCourseCommand command);

    Task<OperationResult> AddSection(AddCourseSectionCommand command);

    Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command);

    Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command);

    Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command);

    Task<OperationResult> EditEpisode(EditEpisodeCommand command);

    Task<CourseFilterResult> GetCourseByFilter(CourseFilterParams filterParams);

    Task<CourseDto?> GetCourseById(Guid courseId);

    Task<EpisodeDto?> GetEpisodeById(Guid episodeId);

    Task<CourseDto?> GetCourseBySlug(string slug);
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

    public async Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> EditEpisode(EditEpisodeCommand command)
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

    public async Task<EpisodeDto?> GetEpisodeById(Guid episodeId)
    {
        return await mediator.Send(new GetEpisodeByIdQuery(episodeId));
    }

    public async Task<CourseDto?> GetCourseBySlug(string slug)
    {
        return await mediator.Send(new GetCourseBySlugQuery(slug));
    }
}