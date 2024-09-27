using CoreModule.Domain.Teachers.DomainServices;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teacher;

public class TeacherDomainService(ITeacherRepository repository):ITeacherDomainService
{
    public bool IsExistsUserName(string userName)
    {
        return repository.Exists(x => x.UserName == userName.ToLower());
    }
}