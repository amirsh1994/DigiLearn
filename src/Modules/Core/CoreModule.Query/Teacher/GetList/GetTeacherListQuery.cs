using Common.Query;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teacher.GetList;

public class GetTeacherListQuery:IBaseQuery<List<TeacherDto>>
{
    
}


internal class GetTeacherListQueryHandler(QueryContext db):IBaseQueryHandler<GetTeacherListQuery,List<TeacherDto>>
{
    public async Task<List<TeacherDto>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
    {
         return await db.Teachers.Include(x=>x.User).Select(x => new TeacherDto
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            UserName = x.UserName,
            Status = x.Status,
            CvFileName = x.CvFileName,
            User = new CoreModuleUserDto
            {
                Id = x.User.Id,
                CreationDate = x.User.CreationDate,
                Avatar = x.User.Avatar,
                Name = x.User.Name,
                Family = x.User.Family,
                Email = x.User.Email,
                PhoneNumber = x.User.phoneNumber
            }
        }).ToListAsync(cancellationToken);
    }
}