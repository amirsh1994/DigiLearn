using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Utils;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Course.Episodes.Add;

public class AddCourseEpisodeCommand : IBaseCommand
{
    public Guid CourseId { get; set; }

    public Guid SectionId { get; set; }

    public string Title { get; set; }

    public string EnglishTitle { get; set; }

    public TimeSpan TimeSpan { get; set; }

    public IFormFile VideoFile { get; set; }

    public IFormFile? AttachmentFile { get; set; }

    public bool IsActive { get; set; }
}


public class AddCourseEpisodeCommandHandler(ICourseRepository repository, IFtpFileService ftp, ILocalFileService localFileService) : IBaseCommandHandler<AddCourseEpisodeCommand>
{
    public async Task<OperationResult> Handle(AddCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);

        if (course == null)
        {
            return OperationResult.NotFound();
        }

        string attExName = "";

        if (request.AttachmentFile!=null && request.AttachmentFile.IsValidCompressFile())
        {
            attExName=Path.GetExtension(request.AttachmentFile.FileName);
        }
        var episode = course.AddEpisode
        (
            request.Title, Guid.NewGuid(), request.TimeSpan,
            Path.GetExtension(request.VideoFile.FileName), attExName
            , request.IsActive, request.SectionId, request.EnglishTitle.ToSlug()
            );
        await SaveFile(request, episode);
        await repository.Save();
        return OperationResult.Success();
    }

    private async Task SaveFile(AddCourseEpisodeCommand request, Episode episode)
    {
        await ftp.SaveFile(request.VideoFile.OpenReadStream(), CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.VideoName);
        if (request.AttachmentFile != null)
        {
            if (request.AttachmentFile.IsValidCompressFile())
            {
                await ftp.SaveFile(request.AttachmentFile.OpenReadStream(), CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.AttachmentName!);
            }
        }
    }
}


public class AddCourseEpisodeCommandValidator : AbstractValidator<AddCourseEpisodeCommand>
{
    public AddCourseEpisodeCommandValidator()
    {

    }
}