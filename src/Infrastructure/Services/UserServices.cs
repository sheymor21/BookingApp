using Domain.DTO.Users;
using Domain.Interfaces;
using Domain.Mappings;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<UserGetRequest?> GetUserByDni(long userDni)
    {
        var user = await _databaseContext.Users.FirstOrDefaultAsync(w => w.Dni == userDni);
        if (user is not null)
        {
            return user.ToUserGetRequest();
        }

        return null;
    }

    public async Task<UserUpdateRequest?> UpdateUser(long userDni, UserUpdateRequest userUpdateRequest)
    {
        var user = await _databaseContext.Users.FirstOrDefaultAsync(w => w.Dni == userDni);
        if (user is null)
        {
            return null;
        }

        user.Name = userUpdateRequest.Name;
        user.LastName = userUpdateRequest.LastName;
        user.Age = userUpdateRequest.Age;
        user.NickName = userUpdateRequest.NickName;
        await _databaseContext.SaveChangesAsync();
        return user.ToUserUpdateRequest();
    }
}