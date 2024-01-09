using Domain.DTO.Users;

namespace Domain.Interfaces;

public interface IUserServices
{
    Task AddUser(UserCreateRequest userCreateRequest);
    Task<UserGetRequest?> GetUserByDni(long userDni);
    Task<UserUpdateRequest?> UpdateUser(long userDni, UserUpdateRequest userUpdateRequest);
}