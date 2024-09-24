using Common.Application;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;
using FluentValidation;

namespace CoreModule.Application.Category.AddChild;

public class AddChildCourseCategoryCommand:IBaseCommand
{
    public Guid ParentId { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }
}


public class AddChildCourseCategoryCommandHandler(ICourseCategoryRepository repository,ICategoryDomainService domainService):IBaseCommandHandler<AddChildCourseCategoryCommand>
{
    public async Task<OperationResult> Handle(AddChildCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, request.ParentId,domainService);
        repository.Add(category);
        await repository.Save();
        return OperationResult.Success();

    }
}


public class AddChildCourseCategoryCommandValidator:AbstractValidator<AddChildCourseCategoryCommand>
{
    public AddChildCourseCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty();
    }
}