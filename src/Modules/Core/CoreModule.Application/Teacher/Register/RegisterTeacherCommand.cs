using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Teachers.DomainServices;
using CoreModule.Domain.Teachers.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Teacher.Register;

public class RegisterTeacherCommand:IBaseCommand
{
    public Guid UserId { get;  set; }

    public string UserName { get;  set; }

    public IFormFile CvFileName { get;  set; }

}

public class RegisterTeacherCommandHandler(ITeacherRepository repository,ITeacherDomainService domainService,ILocalFileService localFileService):IBaseCommandHandler<RegisterTeacherCommand>
{
    public async Task<OperationResult> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var cvFileName =await localFileService.SaveFileAndGenerateName(request.CvFileName,CoreModuleDirectories.CvFileName);
        var teacher = new Domain.Teachers.Models.Teacher(request.UserId, request.UserName,cvFileName,domainService);
        repository.Add(teacher);
        await repository.Save();
        return OperationResult.Success();
    }
}


public class RegisterTeacherCommandValidator:AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherCommandValidator()
    {
        RuleFor(x => x.CvFileName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.UserName)
            .NotNull()
            .NotEmpty();
    }
}