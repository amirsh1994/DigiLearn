using Common.Application;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.Category.Delete;

public record DeleteCourseCategoryCommand(Guid CourseCategoryId) : IBaseCommand;


public class DeleteCourseCategoryCommandHandler(ICourseCategoryRepository repository):IBaseCommandHandler<DeleteCourseCategoryCommand>
{
    public async Task<OperationResult> Handle(DeleteCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetTracking(request.CourseCategoryId);
        if (category == null)
        {
            return OperationResult.NotFound();
        }
        await repository.Delete(category);
        return OperationResult.Success();
    }
}