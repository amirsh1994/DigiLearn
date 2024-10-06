using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Application.Course.Episodes.Add;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Course.Episodes.Edit;

public class EditEpisodeCommand : IBaseCommand
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public Guid SectionId { get; set; }

    public string Title { get; set; }

    public TimeSpan TimeSpan { get; set; }

    public IFormFile? VideoFile { get; set; }

    public IFormFile? AttachmentFile { get; set; }

    public bool IsActive { get; set; }
}


public class EditEpisodeCommandHandler(ICourseRepository repository, ILocalFileService localFileService) : IBaseCommandHandler<EditEpisodeCommand>
{
    public async Task<OperationResult> Handle(EditEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);

        if (course == null)
        {
            return OperationResult.NotFound();
        }

        var episode = course.GetEpisodeById(request.SectionId, request.Id);
        if (episode == null)
        {
            return OperationResult.NotFound();
        }

        string? atName = null;

        if (request.AttachmentFile != null)
        {
         atName=await SaveAttachment(request.AttachmentFile,episode ,course.Id);
        }

        if (request.VideoFile!=null)
        {
            await SaveVideoFile(request.VideoFile,episode,course.Id);
        }

        course.EditEpisode(request.Id, request.SectionId, request.Title, request.IsActive, request.TimeSpan, atName);

        await repository.Save();

        return OperationResult.Success();
    }

    private async Task<string?> SaveAttachment(IFormFile attachment, Episode episode,Guid courseId)
    {

        if (attachment.IsValidCompressFile())
        {
            var attName = episode.VideoName.Replace("mp4", Path.GetExtension(attachment.FileName));
            await localFileService.SaveFile(attachment, CoreModuleDirectories.CourseEpisode(courseId,episode.Token),attName);
            return attName;
        }
        return null;
    }


    private async Task SaveVideoFile(IFormFile videoFile, Episode episode, Guid courseId)
    {
        if (videoFile.IsValidMp4File())
        {
            await localFileService.SaveFile(videoFile, CoreModuleDirectories.CourseEpisode(courseId, episode.Token),episode.VideoName);
        }
    }
}



public class EditEpisodeCommandValidator : AbstractValidator<EditEpisodeCommand>
{
    public EditEpisodeCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
    }
}