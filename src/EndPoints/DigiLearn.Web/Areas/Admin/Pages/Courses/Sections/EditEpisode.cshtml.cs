using CoreModule.Facade.Course;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Course.Episodes.Edit;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure.Utils.CustomValidation.IFormFile;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Sections;


public class EditEpisodeModel(ICourseFacade courseFacade) : BaseRazor
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Title { get; set; }


    [Display(Name = "مدت زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
    [BindProperty]
    public TimeSpan Time { get; set; }


    [Display(Name = "ویدیو")]
    [FileType("mp4", ErrorMessage = "فرمت ویدیو نامعتبر می باشد..")]
    [BindProperty]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "فایل ضمیمه")]
    [FileType("rar", ErrorMessage = "فایل نامعتبر میباشد")]
    [BindProperty]
    public IFormFile? AttachmentFile { get; set; }

    [BindProperty]
    public bool IsActive { get; set; }


    public string?  VideoFileName { get; set; }


    public EpisodeDto? Episode { get; set; } = new();


    public Guid ? CourseId { get; set; }


    public async Task<IActionResult> OnGet(Guid episodeId,Guid courseId)
    {
        var episode = await courseFacade.GetEpisodeById(episodeId);
        if (episode==null)
        {
            return RedirectToPage("../Index", new { courseId });
        }

        Title = episode.Title;
        IsActive=episode.IsActive;
        Time = episode.TimeSpan;
        VideoFileName = episode.VideoName;
        Episode=episode;
        CourseId = courseId;

        return Page();
    }

    public async Task<IActionResult> OnPost(Guid episodeId,Guid courseId)
    {
        var episode=await courseFacade.GetEpisodeById(episodeId);
        var result = await courseFacade.EditEpisode(new EditEpisodeCommand
        {
            Id = episodeId,
            CourseId = courseId,
            SectionId = episode!.SectionId,
            Title = Title,
            TimeSpan = Time,
            VideoFile = VideoFile,
            AttachmentFile = AttachmentFile,
            IsActive = IsActive
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index", new { courseId }));
    }


}

