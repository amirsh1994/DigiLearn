using Common.Application;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Course.Episodes.Accept;

public record AcceptCourseEpisodeCommand(Guid CourseId, Guid EpisodeId) : IBaseCommand;




internal class AcceptCourseEpisodeCommandHandler(ICourseRepository repository):IBaseCommandHandler<AcceptCourseEpisodeCommand>
{
    public async Task<OperationResult> Handle(AcceptCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);

        if (course==null)
        {
            return OperationResult.NotFound();
        }
        course.AcceptEpisode(request.EpisodeId);
        await repository.Save();
        return OperationResult.Success();
    }
}