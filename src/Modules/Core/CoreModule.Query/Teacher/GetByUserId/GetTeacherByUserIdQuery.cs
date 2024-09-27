using Common.Query;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teacher.GetByUserId;

public record GetTeacherByUserIdQuery(Guid UserId) : IBaseQuery<TeacherDto?>;





internal class GetTeacherByUserIdQueryHandler(QueryContext db) : IBaseQueryHandler<GetTeacherByUserIdQuery, TeacherDto?>
{
    public async Task<TeacherDto?> Handle(GetTeacherByUserIdQuery request, CancellationToken cancellationToken)
    {
        var model = await db.Teachers.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        if (model == null)
        {
            return null;
        }

        return new TeacherDto
        {
            Id = model.Id,
            CreationDate = model.CreationDate,
            UserName = model.UserName,
            Status = model.Status,
            CvFileName = model.CvFileName,
            User = new CoreModuleUserDto
            {
                Id = model.User.Id,
                CreationDate = model.User.CreationDate,
                Avatar = model.User.Avatar,
                Name = model.User.Name,
                Family = model.User.Family,
                Email = model.User.Email,
                PhoneNumber = model.User.phoneNumber
            }
        };
    }
}