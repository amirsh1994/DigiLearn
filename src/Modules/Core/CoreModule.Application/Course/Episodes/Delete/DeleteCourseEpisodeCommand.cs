using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Course.Episodes.Delete;

public record DeleteCourseEpisodeCommand(Guid CourseId, Guid EpisodeId) : IBaseCommand;




public class DeleteCourseEpisodeCommandHandler(ICourseRepository repository,ILocalFileService localFileService,IFtpFileService ftp):IBaseCommandHandler<DeleteCourseEpisodeCommand>
{
    public async Task<OperationResult> Handle(DeleteCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);

        if (course==null)
        {
            return OperationResult.NotFound();
        }
        var episode = course.RemoveEpisode(request.EpisodeId);
        await repository.Save();
        localFileService.DeleteDirectory(CoreModuleDirectories.CourseEpisode(course.Id,episode.Token));
        return OperationResult.Success();
    }
}



