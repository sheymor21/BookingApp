using Domain.DTO.Users;
using Domain.Mappings;
using Microsoft.EntityFrameworkCore;
using test.Data;

namespace test;

[Collection("Database")]
public class UserTest
{
    private readonly UserServices _userServices;
    private readonly Fixture _fixture;
    private readonly CreatorManager _creator;
    private readonly DatabaseAppContext _appContextFixture;

    public UserTest()
    {
        _fixture = new Fixture();
        _appContextFixture = new DatabaseContextFixture().AppContext;
        _creator = new(_appContextFixture, _fixture);
        _userServices = new(_appContextFixture);
    }

    [Fact]
    public async Task UserAddAsyncTest()
    {
        User user = _fixture.Build<User>()
            .Without(x => x.Bookings)
            .Without(x => x.BookingsStatus)
            .Create();

        UserCreateRequest userCreateRequest = new UserCreateRequest
        {
            FirstName = user.Name!,
            LastName = user.LastName!,
            NickName = user.NickName!,
            Email = user.Email!,
            Age = user.Age,
            Dni = user.Dni
        };

        await _userServices.AddUserAsync(userCreateRequest);

        var dbUser = await _appContextFixture.Users.FirstOrDefaultAsync(w => w.Dni == userCreateRequest.Dni);
        dbUser.Should().NotBeNull().And.BeEquivalentTo(user, config => config.Excluding(x => x.UserId));
    }

    [Fact]
    public async Task UserUpdateAsyncTest()
    {
        var user = await _creator.UserFixtureGeneratorAsync();
        var userUpdateRequest = user.ToUserUpdateRequest();
        userUpdateRequest!.FirstName = _fixture.Create<string>();
        userUpdateRequest.LastName = _fixture.Create<string>();
        userUpdateRequest.Age = _fixture.Create<int>();
        userUpdateRequest.Email = _fixture.Create<string>();
        userUpdateRequest.NickName = _fixture.Create<string>();
        await _userServices.UpdateUserAsync(user.Dni, userUpdateRequest);

        var dbUser = await _appContextFixture.Users.FirstOrDefaultAsync(w => w.Email == userUpdateRequest.Email);
        dbUser.Should().BeEquivalentTo(userUpdateRequest, config =>
            config.WithMapping<User>(s => s.FirstName, t => t.Name)
        );
    }

    [Fact]
    public async Task UserGetAsyncTest()
    {
        var user = await _creator.UserFixtureGeneratorAsync();
        var userDto = await _userServices.GetUserByDniAsync(user.Dni);
        userDto.Should().BeEquivalentTo(user, config =>
            config.WithMapping<UserGetRequest>(s => s.Name, t => t.FirstName)
                .Excluding(s => s.Bookings)
                .Excluding(s => s.BookingsStatus)
                .Excluding(s => s.UserId)
        );
    }
}