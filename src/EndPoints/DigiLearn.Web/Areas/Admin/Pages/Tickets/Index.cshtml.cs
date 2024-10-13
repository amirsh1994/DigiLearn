using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Tickets;

public class IndexModel(ITicketService ticketService) : BaseRazorFilter<TicketFilterParams>
{
    public TicketFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterParams.Take = 2;
        FilterResult = await ticketService.GetTicketsByFilter(FilterParams);

    }
}

