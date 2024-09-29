using System.ComponentModel.DataAnnotations;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using CoreModule.Application.Course.Create;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses;

[BindProperties]
[ServiceFilter(typeof(TeacherActionFilter))]
public class AddModel(ICourseFacade courseFacade, ITeacherFacade teacherFacade) : BaseRazor
{
    [Display(Name = "دسته بندی اصلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public Guid CategoryId { get; set; }


    [Display(Name = "زیر دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public Guid SubCategoryId { get; set; }


    [Display(Name = "عنوان انگلیسی دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }


    [Display(Name = " عنوان دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "توضیحات دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }


    [Display(Name = "عکس دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس دوره نامعتبر می باشد")]
    public IFormFile ImageFile { get; set; }


    [Display(Name = "دمو دوره")]
    [FileType("mp4",ErrorMessage = "ویدیو معرفی نامعتبز هست")]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "قیمت دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }


    [Display(Name = "سطح دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public CourseLevel CourseLevel { get; set; }



    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var teacher = await teacherFacade.GetTeacherByUserId(User.GetUserId());

        var command = new CreateCourseCommand
        {
            TeacherId = teacher!.Id,
            CategoryId = CategoryId,
            SubCategoryId = SubCategoryId,
            Slug = Slug.ToSlug(),
            Title = Title,
            Description = Description,
            ImageFile = ImageFile,
            VideoFile = VideoFile,
            Price = Price,
            CourseLevel = CourseLevel,
            Status = CourseActionStatus.Pending,
            SeoData = new SeoData(Title, Title, Title, null)
        };
        var result = await courseFacade.Create(command);
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

