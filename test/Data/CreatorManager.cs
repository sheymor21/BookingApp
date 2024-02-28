using AutoFixture;

namespace test.Data;

public class CreatorManager
{
    private readonly Infrastructure.Context.DatabaseAppContext _appContextFixture;
    private readonly Fixture _fixture;

    public CreatorManager(Infrastructure.Context.DatabaseAppContext appContextFixture, Fixture fixture)
    {
        _appContextFixture = appContextFixture;
        _fixture = fixture;
    }

    public async Task<User> UserFixtureGenerator()
    {
        User user = _fixture.Build<User>()
            .Without(x=>x.Bookings)
            .Without(x=>x.BookingsStatus)
            .Create();
        await _appContextFixture.Users.AddAsync(user);
        await _appContextFixture.SaveChangesAsync();
        return user;
    }
}