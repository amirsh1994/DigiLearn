using Common.Application.FileUtil.Interfaces;
using CoreModule.Facade.Category;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Controllers;

[Route("/Ajax/GetCategoryChildren")]
public class AjaxController(ICourseCategoryFacade categoryFacade,ILocalFileService fileService):Controller
{
    public async Task<IActionResult>GetCategoryChildren(Guid id)
    {
        string text = "";

        var children = await categoryFacade.GetChildren(id);

        foreach (var c in children)
        {
            text += $"<option value='{c.Id}'>{c.Title}</option>";
        }
        return Json(text);
    }

    [Route("/Upload/ImageUploader")]
    public async Task<IActionResult> UploadImage(IFormFile?  upload)
    {
        if (upload==null)
        {
            return null;
        }

        var fileName = await fileService.SaveFileAndGenerateName(upload, "wwwroot/images/upload");
        var url = $"/images/upload/{fileName}";
        return Json(new { uploaded = true, url });
    }
}

