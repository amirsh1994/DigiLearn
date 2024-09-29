using Common.Application;
using CoreModule.Application.Category.AddChild;
using CoreModule.Application.Category.Create;
using CoreModule.Application.Category.Delete;
using CoreModule.Application.Category.Edit;
using CoreModule.Query.Category._DTOs;
using CoreModule.Query.Category.GetAll;
using CoreModule.Query.Category.GetChildren;
using MediatR;

namespace CoreModule.Facade.Category;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCourseCategoryCommand command);

    Task<OperationResult> Delete(DeleteCourseCategoryCommand command);

    Task<OperationResult> Edit(EditCourseCategoryCommand command);

    Task<OperationResult> AddChild(AddChildCourseCategoryCommand command);

    Task<List<CourseCategoryDto>> GetMainCategories();

    Task<List<CourseCategoryDto>> GetChildren(Guid parentId);
}


public class CourseCategoryFacade(IMediator mediator):ICourseCategoryFacade
{
    public  async Task<OperationResult> Create(CreateCourseCategoryCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteCourseCategoryCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCategoryCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> AddChild(AddChildCourseCategoryCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<List<CourseCategoryDto>> GetMainCategories()
    {
        return await mediator.Send(new GetAllCourseCategoryQuery());
    }

    public async Task<List<CourseCategoryDto>> GetChildren(Guid parentId)
    {
        return await mediator.Send(new GetCourseCategoryChildrenQuery(parentId));
    }
}