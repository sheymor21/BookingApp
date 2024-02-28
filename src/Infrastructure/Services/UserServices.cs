using Domain.DTO.Users;
using Domain.Interfaces;
using Domain.Mappings;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserServices : IUserServices
{
    private readonly DatabaseAppContext _databaseAppContext;

    public UserServices(DatabaseAppContext databaseAppContext)
    {
        _databaseAppContext = databaseAppContext;
    }

    public async Task AddUserAsync(UserCreateRequest userCreateRequest)
    {
        await _databaseAppContext.Users.AddAsync(userCreateRequest.ToUser());
        await _databaseAppContext.SaveChangesAsync();
    }

    public async Task<UserGetRequest?> GetUserByDniAsync(long userDni)
    {
        var user = await _databaseAppContext.Users.FirstOrDefaultAsync(w => w.Dni == userDni);
        if (user is not null)
        {
            return user.ToUserGetRequest();
        }

        return null;
    }

    public async Task<UserUpdateRequest?> UpdateUserAsync(long userDni, UserUpdateRequest userUpdateRequest)
    {
        var user = await _databaseAppContext.Users.FirstOrDefaultAsync(w => w.Dni == userDni);
        if (user is null)
        {
            return null;
        }

        user.Name = userUpdateRequest.FirstName;
        user.LastName = userUpdateRequest.LastName;
        user.Age = userUpdateRequest.Age;
        user.NickName = userUpdateRequest.NickName;
        user.Email = userUpdateRequest.Email;
        await _databaseAppContext.SaveChangesAsync();
        return user.ToUserUpdateRequest();
    }
}