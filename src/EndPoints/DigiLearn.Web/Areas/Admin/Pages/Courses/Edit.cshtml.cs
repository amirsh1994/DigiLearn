using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Course.Edit;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;
using DigiLearn.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses;

[BindProperties]
public class EditModel(ICourseFacade courseFacade) : BaseRazor
{

    public Guid CategoryId { get; set; }

    public Guid SubCategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }


    [Display(Name = "عکس")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "ویدئو معرفی")]
    [FileType("mp4", ErrorMessage = "ویدئو معرفی نامعتبر است")]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "قیمت")]
    public int Price { get; set; }
    public SeoDataViewModel SeoData { get; set; }



    [Display(Name = "سطح دوره")]
    public CourseLevel CourseLevel { get; set; }


    [Display(Name = "وضعیت دوره")]
    public CourseStatus CourseStatus { get; set; }


    [Display(Name = "وضعیت ")]
    public CourseActionStatus CourseActionStatus { get; set; }

    //public string?  ImageFileName { get; set; }


    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await courseFacade.GetCourseById(courseId);
        if (course==null)
        {
            return RedirectToPage("Index");
        }

        CategoryId = course.CategoryId;
        SubCategoryId=course.SubCategoryId;
        Price = course.Price;
        Slug=course.Slug;
        Title=course.Title;
        Description=course.Description;
        SeoData=SeoDataViewModel.ToViewModel(course.SeoData);
        CourseLevel = course.CourseLevel;
        CourseStatus=course.CourseStatus;
        CourseActionStatus = course.Status;
        //ImageFileName = course.ImageName;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await courseFacade.Edit(new EditCourseCommand
        {
            CourseId = courseId,
            CategoryId = CategoryId,
            SubCategoryId = SubCategoryId,
            Slug = Slug,
            Title = Title,
            Description = Description,
            ImageFile = ImageFile,
            VideoFileName = VideoFile,
            Price = Price,
            CourseLevel = CourseLevel,
            CourseStatus = CourseStatus,
            SeoData = SeoData.Map(),
            ActionStatus = CourseActionStatus
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

