using AutoMapper;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Queries._DTOs;
using UserModule.Data;

namespace UserModule.Core.Queries.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IBaseQuery<UserDto?>;

    


public class GetUserByPhoneNumberQueryHandler(UserContext db,IMapper mapper):IBaseQueryHandler<GetUserByPhoneNumberQuery,UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.phoneNumber == request.PhoneNumber, cancellationToken: cancellationToken);
        if (user == null)
        {
            return null;
        }

        return mapper.Map<UserDto>(user);
    }
}