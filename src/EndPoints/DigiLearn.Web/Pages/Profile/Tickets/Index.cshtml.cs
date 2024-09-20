using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
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
            Take = 2,
            UserId = User.GetUserId()
        });
        FilterResult = result;
    }
}

