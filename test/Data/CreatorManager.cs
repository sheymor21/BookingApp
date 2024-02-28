using Domain.Models.BookingModels;

namespace test.Data;

public class CreatorManager
{
    private readonly DatabaseAppContext _appContextFixture;
    private readonly Fixture _fixture;

    public CreatorManager(DatabaseAppContext appContextFixture, Fixture fixture)
    {
        _appContextFixture = appContextFixture;
        _fixture = fixture;
    }

    public async Task<User> UserFixtureGeneratorAsync()
    {
        User user = _fixture.Build<User>()
            .Without(x => x.Bookings)
            .Without(x => x.BookingsStatus)
            .Create();
        await _appContextFixture.Users.AddAsync(user);
        await _appContextFixture.SaveChangesAsync();

        return user;
    }

    public async Task<List<User>> UserFixtureGeneratorAsync(int quantity)
    {
        IEnumerable<User> users = _fixture.Build<User>()
            .Without(x => x.Bookings)
            .Without(x => x.BookingsStatus)
            .CreateMany(quantity);
        var userList = users.ToList();
        await _appContextFixture.Users.AddRangeAsync(userList);
        await _appContextFixture.SaveChangesAsync();

        return userList;
    }

    public async Task<Booking> BookingFixtureGeneratorAsync(User user)
    {
        Booking booking = _fixture.Build<Booking>()
            .With(x => x.User, user)
            .Without(x => x.BookingUserStatus)
            .Create();
        await _appContextFixture.Bookings.AddAsync(booking);
        await _appContextFixture.SaveChangesAsync();

        return booking;
    }

    public async Task<BookingUserStatus> BookingUserStatusFixtureGenerator(Booking booking, User user)
    {
        BookingUserStatus bookingUserStatus = _fixture.Build<BookingUserStatus>()
            .Without(x => x.BookingCancelleds)
            .With(x => x.User, user)
            .With(x => x.Booking, booking)
            .Create();
        await _appContextFixture.BookingUserStatus.AddAsync(bookingUserStatus);
        await _appContextFixture.SaveChangesAsync();
        return bookingUserStatus;
    }
}