using Domain.DTO.Users;
using Domain.Models.UserModels;

namespace Domain.Mappings;

public static class UserMappings
{
    public static User ToUser(this UserCreateRequest userCreateRequest)
    {
        var map = new User
        {
            Name = userCreateRequest.Name,
            LastName = userCreateRequest.LastName,
            NickName = userCreateRequest.NickName,
            Dni = userCreateRequest.Dni,
            Password = userCreateRequest.Password
        };
        return map;
    }
}