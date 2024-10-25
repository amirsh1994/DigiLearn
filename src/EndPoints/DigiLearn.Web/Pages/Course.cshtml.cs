using CoreModule.Application._Utilities;
using CoreModule.Facade.Course;
using CoreModule.Query.Course._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class CourseModel(ICourseFacade courseFacade) : PageModel
{
    public CourseDto? Course { get; set; }

    public async Task<IActionResult> OnGet(string slug)
    {
        var course = await courseFacade.GetCourseBySlug(slug);

        if (course == null)
        {
            return NotFound();
        }

        Course = course;
        return Page();
    }

    public async Task<IActionResult> OnGetShowOnline(string slug, Guid sectionId, Guid token)
    {
        var course = await courseFacade.GetCourseBySlug(slug);
        if (course == null)
        {
            return NotFound();
        }

        var section = course.Sections.First(x => x.Id == sectionId);
        var episode= section.Episodes.FirstOrDefault(x => x.Token == token);
        if (episode == null)
        {
            return NotFound();
        }
        return Content(@CoreModuleDirectories.GetEpisodeFile(course.Id,token,episode.VideoName));

    }

    public async Task<IActionResult> OnGetDownloadEpisode(string slug, Guid sectionId, Guid token)
    {
        var course = await courseFacade.GetCourseBySlug(slug);
        if (course == null)
        {
            return NotFound();
        }

        var section = course.Sections.First(x => x.Id == sectionId);
        var episode = section.Episodes.FirstOrDefault(x => x.Token == token);
        if (episode == null)
        {
            return NotFound();
        }

        var fileName = Path.Combine(Directory.GetCurrentDirectory(),CoreModuleDirectories.CourseEpisode(course.Id, token), episode.VideoName);
        var file = new FileStream(fileName, FileMode.Open);
        return File(file, "application/force-download", episode.VideoName);

    }
}

