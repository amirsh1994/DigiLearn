using AutoMapper;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Data;
using TicketModule.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketModule.Core.Services;

public interface ITicketService
{
    Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);

    Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command);

    Task<OperationResult> CloseTicket(Guid ticketId);

    Task<TicketDto> GetTicket(Guid ticketId);


}

internal class TicketService(TicketContext db, IMapper mapper) : ITicketService
{


    public async Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command)
    {
        var ticket = mapper.Map<Ticket>(command);
        db.Tickets.Add(ticket);
        await db.SaveChangesAsync();
        return OperationResult<Guid>.Success(ticket.Id);
    }

    public async Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command)
    {
        var ticket = await db.Tickets.FirstOrDefaultAsync(x => x.Id == command.TicketId);
        if (ticket == null)
        {
            return OperationResult.NotFound();
        }

        var message = new TicketMessage()
        {
            Text = command.Text.SanitizeText()
            ,
            TicketId = command.TicketId
            ,
            UserID = command.UserId
            ,
            UserFullName = command.OwnerFullName
        };
        ticket.TicketStatus = ticket.UserId==command.UserId ? TicketStatus.Pending : TicketStatus.Answered; //خود کاربر داره پیام رو ارسال میکنه
        db.TicketMessages.Add(message);
        db.Tickets.Update(ticket);
        await db.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult> CloseTicket(Guid ticketId)
    {
        var ticket = await db.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);
        if (ticket == null)
        {
            return OperationResult.NotFound();
        }

        ticket.TicketStatus = TicketStatus.Closed;
        db.Tickets.Update(ticket);
        await db.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<TicketDto> GetTicket(Guid ticketId)
    {
       var ticket=await db.Tickets
           .Include(x=>x.TicketMessages)
           .AsSplitQuery()
           .FirstOrDefaultAsync(x=>x.Id== ticketId);

       return mapper.Map<TicketDto>(ticket);
    }
}