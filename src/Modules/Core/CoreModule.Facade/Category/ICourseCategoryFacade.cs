﻿using Common.Application;
using CoreModule.Application.Category.AddChild;
using CoreModule.Application.Category.Create;
using CoreModule.Application.Category.Delete;
using CoreModule.Application.Category.Edit;
using MediatR;

namespace CoreModule.Facade.Category;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCourseCategoryCommand command);

    Task<OperationResult> Delete(DeleteCourseCategoryCommand command);

    Task<OperationResult> Edit(EditCourseCategoryCommand command);

    Task<OperationResult> AddChild(AddChildCourseCategoryCommand command);
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
}