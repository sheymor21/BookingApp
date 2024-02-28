using Bogus;
using Domain.DTO.Users;
using Domain.Interfaces;
using Domain.Mappings;
using Domain.Models.BookingModels;
using Domain.Models.UserModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DummyServices : IDummyServices
{
    private readonly DatabaseAppContext _databaseAppContext;

    public DummyServices(DatabaseAppContext databaseAppContext)
    {
        _databaseAppContext = databaseAppContext;
    }

    public async Task DummyConstructor(int quantity)
    {
        var fUser = SetUsers().Generate(quantity);
        foreach (var item in fUser)
        {
            item.Bookings!.AddRange(SetBooking().Generate(quantity));
        }

        await _databaseAppContext.Users.AddRangeAsync(fUser);
        await _databaseAppContext.SaveChangesAsync();
    }

    public async IAsyncEnumerable<UserGetRequest> GetAllUser()
    {
        var users = _databaseAppContext.Users.AsTracking().AsAsyncEnumerable();
        await foreach (var item in users)
        {
            yield return item.ToUserGetRequest()!;
        }
    }

    private Faker<User> SetUsers()
    {
        var userGeneratorRules = new Faker<User>()
            .RuleFor(x => x.Dni, f => f.Random.Number(min: 111111111, max: 999999999))
            .RuleFor(x => x.Age, f => f.Random.Number(min: 18, max: 80))
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.NickName, f => f.Lorem.Word());

        return userGeneratorRules;
    }

    private Faker<Booking> SetBooking()
    {
        var fBookingCancelled = SetBookingCancelled().Generate();
        var fbookingUserStatusList = SetBookingUserStatus(SetUsers().Generate()).Generate(2);
        fbookingUserStatusList[0].BookingCancelleds.Add(fBookingCancelled);
        var bookingGeneratorRules = new Faker<Booking>()
            .RuleFor(x => x.StartDate, f => f.Date.Future().ToUniversalTime())
            .RuleFor(x => x.EndDate, (f, x) => f.Date.Between(x.StartDate, x.StartDate.AddDays(7)).ToUniversalTime())
            .RuleFor(x => x.Cancelled, f => f.Random.Bool())
            .RuleFor(x => x.BookingUserStatus, fbookingUserStatusList);


        return bookingGeneratorRules;
    }

    private Faker<BookingUserStatus> SetBookingUserStatus(User user)
    {
        var bookingUserStatusGenerator = new Faker<BookingUserStatus>()
            .RuleFor(x => x.User, user)
            .RuleFor(x => x.Accepted, f => f.Random.Bool());

        return bookingUserStatusGenerator;
    }

    private Faker<BookingCancelled> SetBookingCancelled()
    {
        var bookingCancelledGenerator = new Faker<BookingCancelled>()
            .RuleFor(x => x.Reason, f => f.Lorem.Sentence());

        return bookingCancelledGenerator;
    }
}