using AutoMapper;
using UserModule.Core.Queries._DTOs;
using UserModule.Data.Entities.Users;

namespace UserModule.Core;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<User,UserDto>().ReverseMap();
    }
}