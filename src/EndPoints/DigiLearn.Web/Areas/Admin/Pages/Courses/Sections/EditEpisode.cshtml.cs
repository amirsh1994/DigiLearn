using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Sections;


[BindProperties]
public class EditEpisodeModel(ICourseFacade courseFacade) : BaseRazor
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "مدت زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
    public TimeSpan Time { get; set; }


    [Display(Name = "ویدیو")]
    [FileType("mp4", ErrorMessage = "فرمت ویدیو نامعتبر می باشد..")]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "فایل ضمیمه")]
    [FileType("rar", ErrorMessage = "فایل نامعتبر میباشد")]
    public IFormFile? AttachmentFile { get; set; }


    public bool IsActive { get; set; }


    public async Task OnGet(Guid sectionId)
    {

    }



}

