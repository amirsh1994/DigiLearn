using Common.Domain;
using MediatR;

namespace CoreModule.Domain.Teachers.Events;

public class RejectRequestDomainEvent:BaseDomainEvent
{
    public string Description { get; set; }

    public Guid UserId { get; set; }
}

public class AcceptRequestDomainEvent:BaseDomainEvent
{

    public Guid UserId { get; set; }
}