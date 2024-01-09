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
            Age = userCreateRequest.Age
        };
        return map;
    }

    public static UserGetRequest? ToUserGetRequest(this User user)
    {
        var map = new UserGetRequest
        {
            Name = user.Name,
            LastName = user.LastName,
            NickName = user.NickName,
            Dni = user.Dni,
            Age = user.Age
        };
        return map;
    }

    public static UserUpdateRequest? ToUserUpdateRequest(this User user)
    {
        var map = new UserUpdateRequest
        {
            Name = user.Name,
            LastName = user.LastName,
            NickName = user.NickName,
            Age = user.Age
        };
        return map;
    }
}