using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Course.Sections.AddSection;
using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel(ICourseFacade courseFacade) : BaseRazor
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "ترتیب نمایش")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int DisplayOrder { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await courseFacade.AddSection(new AddCourseSectionCommand
        {
            CourseId = courseId,
            Title = Title,
            DisplayOrder = DisplayOrder
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index", new{ courseId }));
    }
}

