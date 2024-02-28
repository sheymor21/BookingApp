using Domain.DTO.Users;

namespace Domain.Interfaces;

public interface IUserServices
{
    Task AddUserAsync(UserCreateRequest userCreateRequest);
    Task<UserGetRequest?> GetUserByDniAsync(long userDni);
    Task<UserUpdateRequest?> UpdateUserAsync(long userDni, UserUpdateRequest userUpdateRequest);
}