using Domain.DTO.Users;

namespace Domain.Interfaces;

public interface IUserServices
{
    Task AddUser(UserCreateRequest userCreateRequest);
}