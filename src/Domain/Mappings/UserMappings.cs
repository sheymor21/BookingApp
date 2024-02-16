using Domain.DTO.Users;
using Domain.Models.UserModels;

namespace Domain.Mappings;

public static class UserMappings
{
    public static User ToUser(this UserCreateRequest userCreateRequest)
    {
        var map = new User
        {
            Name = userCreateRequest.FirstName,
            LastName = userCreateRequest.LastName,
            NickName = userCreateRequest.NickName,
            Dni = userCreateRequest.Dni,
            Age = userCreateRequest.Age,
            Email = userCreateRequest.Email
        };
        return map;
    }

    public static UserGetRequest? ToUserGetRequest(this User user)
    {
        var map = new UserGetRequest
        {
            FirstName = user.Name,
            LastName = user.LastName!,
            NickName = user.NickName!,
            Dni = user.Dni,
            Age = user.Age,
            Email = user.Email!
        };
        return map;
    }

    public static UserUpdateRequest? ToUserUpdateRequest(this User user)
    {
        var map = new UserUpdateRequest
        {
            FirstName = user.Name,
            LastName = user.LastName!,
            NickName = user.NickName!,
            Age = user.Age,
            Email = user.Email!
        };
        return map;
    }
}