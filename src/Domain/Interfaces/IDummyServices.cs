using Domain.DTO.Users;

namespace Domain.Interfaces;

public interface IDummyServices
{
    Task DummyConstructor(int quantity);
    IAsyncEnumerable<UserGetRequest> GetAllUser();
}