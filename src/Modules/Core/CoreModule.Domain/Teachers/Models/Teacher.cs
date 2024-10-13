using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.Teachers.DomainServices;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Domain.Teachers.Events;

namespace CoreModule.Domain.Teachers.Models;

public class Teacher:AggregateRoot
{
    public Guid UserId { get; private set; }

    public string UserName { get; private set; }

    public TeacherStatus Status { get; private set; }

    public string CvFileName { get; private set; }

    private Teacher()
    {
        
    }

    public Teacher(Guid userId, string userName, string cvFileName, ITeacherDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(cvFileName, nameof(cvFileName));
        NullOrEmptyDomainDataException.CheckString(userName, nameof(userName));

        if (userName.IsUniCode())
        {
            throw new InvalidDomainDataException("userName must be english...!");
        }

        if (domainService.IsExistsUserName(userName))
        {
            throw new InvalidDomainDataException("duplicated userName is already Exists...!");
        }

        UserId = userId;
        UserName = userName.ToLower();
        Status = TeacherStatus.Pending;
        CvFileName = cvFileName;
    }

    public void ToggleStatus()
    {
        if (Status == TeacherStatus.Active)
        {
            Status = TeacherStatus.InActive;
        }
        else if (Status == TeacherStatus.InActive)
        {
            Status = TeacherStatus.Active;
        }
    }

    public void AcceptRequest()
    {
        if (Status==TeacherStatus.Pending)
        {
            //todo Raise DomainEvent
            AddDomainEvent(new AcceptRequestDomainEvent(){UserId = UserId});
            Status = TeacherStatus.Active;
        }
    }
}