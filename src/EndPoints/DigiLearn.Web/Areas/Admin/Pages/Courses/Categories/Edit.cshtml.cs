using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Utils;
using CoreModule.Application.Category.Edit;
using CoreModule.Facade.Category;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories;

[BindProperties]
public class EditModel(ICourseCategoryFacade categoryFacade) : BaseRazor
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var cat = await categoryFacade.GetById(id);
        if (cat == null)
        {
            return RedirectToPage("Index");
        }

        Title = cat.Title;
        Slug = cat.Slug;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid id)
    {
        var result = await categoryFacade.Edit(new EditCourseCategoryCommand
        {
            Id = id,
            Title = Title,
            Slug = Slug.ToSlug()
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }

}

