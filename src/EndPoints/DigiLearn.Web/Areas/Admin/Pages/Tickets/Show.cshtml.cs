using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Core.Services;
using UserModule.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Tickets;

public class ShowModel(ITicketService ticketService,IUserFacade userFacade) : BaseRazor
{
    public TicketDto? Ticket { get; set; }

    [BindProperty]
    [Display(Name = "متن پیام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Text { get; set; }

    public async Task<IActionResult> OnGet(Guid ticketId)
    {
        var ticket = await ticketService.GetTicket(ticketId);
        if (ticket == null)
            return RedirectToPage("Index");

        Ticket = ticket;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid ticketId)
    {
        var user = await userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        var message = new SendTicketMessageCommand
        {
            UserId = User.GetUserId(),
            TicketId = ticketId,
            OwnerFullName = $"{user!.Name}{user.Family}",
            Text = Text
        };
        var result = await ticketService.SendMessageInTicket(message);
        return RedirectAndShowAlert(result, RedirectToPage("Show", new { ticketId }));
    }

    public async Task<IActionResult> OnPostCloseTicket(Guid ticketId)
    {
        return await AjaxTryCatch(async () =>
        {
            var ticket = await ticketService.GetTicket(ticketId);
            if (ticket == null || ticket.UserId != User.GetUserId())
            {
                return OperationResult.Error("تیکت یافت نشد");
            }
            return await ticketService.CloseTicket(ticketId);
        });
    }
}

