using Common.Domain;

namespace TicketModule.Data.Entities;

internal class Ticket: BaseEntity
{
    public Guid UserId { get; set; }

    public string OwnerFullName { get; set; }

    public string PhoneNumber { get; set; }

    public TicketStatus TicketStatus { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public List<TicketMessage> TicketMessages { get; set; } = [];
}

public enum TicketStatus
{
    Pending,
    Answered,
    Closed
}

internal class TicketMessage: BaseEntity
{
    public Guid UserID { get; set; }

    public Guid TicketId { get; set; }

    public Ticket Ticket { get; set; }

    public string UserFullName { get; set; }

    public string Text { get; set; }

}