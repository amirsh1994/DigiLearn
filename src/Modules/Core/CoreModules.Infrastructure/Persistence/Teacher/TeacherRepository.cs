using Common.Domain.Repository;
using Common.Infrastructure.Repository;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Infrastructure.Persistence.Teacher;

internal class TeacherRepository(CoreModuleEfContext context) : BaseRepository<Domain.Teachers.Models.Teacher, CoreModuleEfContext>(context), ITeacherRepository
{
    public void Delete(Domain.Teachers.Models.Teacher teacher)
    {
        context.Teachers.Remove(teacher);
        context.SaveChanges();
    }
}