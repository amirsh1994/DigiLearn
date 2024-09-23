using Common.Domain.Repository;
using CoreModule.Domain.Teachers.Models;

namespace CoreModule.Domain.Teachers.Repository;

public interface ITeacherRepository:IBaseRepository<Teacher>
{
    void Delete(Teacher teacher);
}