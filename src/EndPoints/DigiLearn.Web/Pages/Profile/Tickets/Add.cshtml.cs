using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Core.Services;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile.Tickets;


[BindProperties]
public class AddModel(ITicketService ticketService,IUserFacade userFacade) : BaseRazor
{

    [Display(Name = "عنوان تیکت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "متن تیکت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.MultilineText)]
    public string Text { get; set; }

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPost()
    {
        var user = await userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        var command = new CreateTicketCommand
        {
            UserId = user!.Id,
            OwnerFullName = $"{user.Name}{user.Family}",
            PhoneNumber = user.phoneNumber,
            Title = Title,
            Text = Text
        };
        var result = await ticketService.CreateTicket(command);
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

