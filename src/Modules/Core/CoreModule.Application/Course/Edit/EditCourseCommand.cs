using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Common.Domain.ValueObjects;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.DomainServices;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Domain.Courses.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Course.Edit;

public class EditCourseCommand : IBaseCommand
{
    public Guid CourseId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid SubCategoryId { get; set; }

    public string Slug { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IFormFile? ImageFile { get; set; }

    public IFormFile? VideoFileName { get; set; }

    public int Price { get; set; }

    public CourseLevel CourseLevel { get; set; }

    public CourseStatus CourseStatus { get; set; }

    public CourseActionStatus ActionStatus { get; set; }

    public SeoData SeoData { get; set; }
}


public class EditCourseCommandHandler(ICourseRepository repository, ILocalFileService localFileService, ICourseDomainService domainService) : IBaseCommandHandler<EditCourseCommand>
{
    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }
        var imageName = course.ImageName;
        var videoPath = course.VideoName;
        var oldImageName = course.ImageName;
        var oldVideoFileName = course.VideoName;
        if (request.VideoFileName != null)
        {
            if (request.VideoFileName.IsValidMp4File() == false)
            {
                return OperationResult.Error("only mp4 file is accepted for edit ");
            }
            videoPath = await localFileService.SaveFileAndGenerateName(request.VideoFileName, CoreModuleDirectories.CourseDemo(course.Id));
        }

        if (request.ImageFile != null)
        {
            if (request.ImageFile.IsImage())
            {
                imageName = await localFileService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImage);
            }
        }
        course.Edit
            (
            request.Title, request.Description, imageName, videoPath, request.Price
            , request.CourseLevel, request.CourseStatus, request.SeoData, request.SubCategoryId
            , request.CategoryId, request.Slug,request.ActionStatus, domainService
            );
        await repository.Save();
        DeleteOldFile(oldImageName, oldVideoFileName, request.ImageFile != null, request.VideoFileName != null, course);
        return OperationResult.Success();
    }

    private void DeleteOldFile(string image, string? video, bool isUploadImage, bool isUploadVideo, Domain.Courses.Models.Course course)
    {
        if (isUploadVideo && string.IsNullOrWhiteSpace(video) == false)
        {
            localFileService.DeleteFile(CoreModuleDirectories.CourseDemo(course.Id), video);
        }

        if (isUploadImage)
        {
            localFileService.DeleteFile(CoreModuleDirectories.CourseImage, image);
        }
    }
}



public class EditCourseCommandValidator : AbstractValidator<EditCourseCommand>
{
    public EditCourseCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty();

        //RuleFor(x => x.ImageFile)
        //    .NotNull();
    }
}