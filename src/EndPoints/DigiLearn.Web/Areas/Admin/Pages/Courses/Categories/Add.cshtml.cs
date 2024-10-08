﻿using System.ComponentModel.DataAnnotations;
using Common.Domain.Utils;
using CoreModule.Application.Category.AddChild;
using CoreModule.Application.Category.Create;
using CoreModule.Facade.Category;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories;

[BindProperties]
public class AddModel(ICourseCategoryFacade categoryFacade) : BaseRazor
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost([FromQuery]Guid ? parentId)
    {
        if (parentId!=null)
        {
            var result = await categoryFacade.AddChild(new AddChildCourseCategoryCommand()
                {
                    Title = Title,
                    Slug = Slug.ToSlug(),
                    ParentId = (Guid)parentId,
                }
            );
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
        else
        {
            var result = await categoryFacade.Create(new CreateCourseCategoryCommand
                {
                    Title = Title,
                    Slug = Slug.ToSlug()
                }
            );
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
       
    }
}

