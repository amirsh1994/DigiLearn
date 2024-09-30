using Common.Application;
using CoreModule.Domain.Courses.Repository;
using FluentValidation;

namespace CoreModule.Application.Course.Sections.AddSection;

public class AddCourseSectionCommand : IBaseCommand
{
    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public int DisplayOrder { get; set; }

}


public class AddCourseSectionCommandHandler(ICourseRepository repository) : IBaseCommandHandler<AddCourseSectionCommand>
{
    public async Task<OperationResult> Handle(AddCourseSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }
        course.AddSection(request.Title, request.DisplayOrder);
        await repository.Save();
        return OperationResult.Success();
    }
}


public class AddCourseSectionCommandValidator:AbstractValidator<AddCourseSectionCommand>
{
    public AddCourseSectionCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
    }
}