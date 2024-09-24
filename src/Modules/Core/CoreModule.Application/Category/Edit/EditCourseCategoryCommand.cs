using AngleSharp.Html.Dom;
using Common.Application;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Courses.Repository;
using FluentValidation;
using FluentValidation.Validators;

namespace CoreModule.Application.Category.Edit;

public class EditCourseCategoryCommand:IBaseCommand
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }
    
}


public class EditCategoryCommandHandler(ICourseCategoryRepository repository,ICategoryDomainService domainService):IBaseCommandHandler<EditCourseCategoryCommand>
{
    public async Task<OperationResult> Handle(EditCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetTracking(request.Id);

        if (category==null)
        {
            return OperationResult.NotFound();
        }
        category.Edit(request.Title,request.Slug,domainService);
        await repository.Save();
        return OperationResult.Success();
    }
}

public class EditCourseCategoryCommandValidator:AbstractValidator<EditCourseCategoryCommand>
{
    public EditCourseCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty();
    }
}