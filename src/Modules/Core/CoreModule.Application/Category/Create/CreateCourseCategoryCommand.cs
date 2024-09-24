using Common.Application;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;
using FluentValidation;

namespace CoreModule.Application.Category.Create;

public class CreateCourseCategoryCommand:IBaseCommand
{

    public string Title { get;  set; }

    public string Slug { get;  set; }

}


public class CreateCategoryCommandHandler(ICourseCategoryRepository repository,ICategoryDomainService domainService):IBaseCommandHandler<CreateCourseCategoryCommand>
{
    public async Task<OperationResult> Handle(CreateCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug,null, domainService);
        repository.Add(category);
        await repository.Save();
        return OperationResult.Success();
    }
}


public class CreateCategoryCommandValidator:AbstractValidator<CreateCourseCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

    }
}