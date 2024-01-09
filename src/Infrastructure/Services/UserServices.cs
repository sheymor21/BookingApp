using Domain.DTO.Users;
using Domain.Interfaces;
using Domain.Mappings;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class UserServices : IUserServices
{
    private readonly DatabaseContext _databaseContext;

    public UserServices(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddUser(UserCreateRequest userCreateRequest)
    {
        await _databaseContext.Users.AddAsync(userCreateRequest.ToUser());
        await _databaseContext.SaveChangesAsync();
    }
}