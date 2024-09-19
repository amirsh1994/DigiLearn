using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile.Tickets;

public class IndexModel(ITicketService ticketService) : BaseRazorFilter<TicketFilterParams>
{
    public TicketFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        var result = await ticketService.GetTicketsByFilter(new TicketFilterParams
        {
            PageId = FilterParams.PageId,
            Take = FilterParams.Take,
            UserId = User.GetUserId()
        });
        FilterResult = result;

    }
}

