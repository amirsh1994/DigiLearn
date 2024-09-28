using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.DomainServices;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Domain.Courses.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Course.Create;

public class CreateCourseCommand : IBaseCommand
{
    public Guid TeacherId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid SubCategoryId { get; set; }

    public string Slug { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IFormFile ImageFile { get; set; }

    public IFormFile? VideoFileName { get; set; }

    public int Price { get; set; }

    public CourseLevel CourseLevel { get; set; }

    public CourseActionStatus Status { get; set; }

    public SeoData SeoData { get; set; }
}


public class CreateCourseCommandHandler(ICourseRepository repository, ICourseDomainService domainService, IFtpFileService ftp, ILocalFileService localFileService) : IBaseCommandHandler<CreateCourseCommand>
{
    public async Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var imageName = await localFileService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImage);
        string? videoPath = null;
        Guid courseId = Guid.NewGuid();

        if (request.VideoFileName != null)
        {
            if (request.VideoFileName.IsValidMp4File() == false)
            {
                return OperationResult.Error("فرمت ویدیو وارد شده نامعتبر می باشد...!");
            }
            videoPath = await ftp.SaveFileAndGenerateName(request.VideoFileName, CoreModuleDirectories.CourseDemo(courseId));
        }
        var course = new Domain.Courses.Models.Course
            (
            request.TeacherId, request.Title, request.Description
            , imageName, videoPath, request.Price, request.CourseLevel
            , request.SeoData, request.SubCategoryId
            , request.CategoryId, request.Slug,request.Status, domainService
            ){ Id = courseId };

        repository.Add(course);
        await repository.Save();
        return OperationResult.Success();
    }
}


public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
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

        RuleFor(x => x.ImageFile)
            .NotNull();
    }
}