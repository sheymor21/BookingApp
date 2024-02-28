using Domain.DTO.Bookings;
using Microsoft.EntityFrameworkCore;
using test.Data;

namespace test;

[Collection("Database")]
public class BookingTest
{
    private readonly BookingServices _bookingServices;
    private readonly Fixture _fixture;
    private readonly CreatorManager _creator;
    private readonly DatabaseAppContext _appContextFixture;

    public BookingTest()
    {
        _appContextFixture = new DatabaseContextFixture().AppContext;
        _bookingServices = new(_appContextFixture);
        _fixture = new();
        _creator = new(_appContextFixture, _fixture);
    }

    [Fact]
    public async Task BookingAddTest()
    {
        var user = await _creator.UserFixtureGeneratorAsync(2);

        BookingCreateRequest bookingCreateRequest = new()
        {
            StartDate = _fixture.Create<DateTime>().ToUniversalTime(),
            EndDate = _fixture.Create<DateTime>().ToUniversalTime(),
            OwnerMail = user[0].Email,
            Invitations = new List<string>
            {
                user[1].Email!
            }
        };

        await _bookingServices.AddBookingAsync(bookingCreateRequest);

        var bookingDb = await _appContextFixture.Bookings
            .AsNoTracking()
            .Include(x => x.BookingUserStatus)
            .ThenInclude(x => x.User)
            .Include(x => x.User)
            .FirstAsync();

        bookingDb.Should().BeEquivalentTo(bookingCreateRequest, config =>
            config.Excluding(x => x.Invitations)
                .Excluding(x => x.OwnerMail)
        );

        bookingDb.User.Email.Should().Be(bookingCreateRequest.OwnerMail);
        bookingDb.BookingUserStatus[0].User.Email.Should().Be(bookingCreateRequest.Invitations[0]);
    }

    [Fact]
    public async Task BookingUpdateTest()
    {
        var user = await _creator.UserFixtureGeneratorAsync();
        var booking = await _creator.BookingFixtureGeneratorAsync(user);
        BookingUpdateRequest bookingUpdateRequest = new()
        {
            StartDate = booking.StartDate,
            EndDate = booking.EndDate
        };
        await _bookingServices.UpdateBookingAsync(booking.BookingId, bookingUpdateRequest);
        
        var bookingDb = await _appContextFixture.Bookings.FirstAsync();
        bookingDb.Should().BeEquivalentTo(bookingUpdateRequest,config=>
            config.Excluding(x=>x.NewInviteds)
                .Excluding(x=>x.DeleteInviteds)
            );
    }

    [Fact]
    public async Task BookingGetTest()
    {
        var user = await _creator.UserFixtureGeneratorAsync();
        var booking = await _creator.BookingFixtureGeneratorAsync(user);
        
        var bookingsDto = await _bookingServices.GetBookingAsync(user.Email!);
        bookingsDto.Should().NotBeNull();
        bookingsDto.Should().HaveCount(1);
        var bookingDto = bookingsDto[0];
        
        bookingDto.StartDate.Should().Be(booking.StartDate.ToUniversalTime());
        bookingDto.EndDate.Should().Be(booking.EndDate.ToUniversalTime());
        bookingDto.OwnerMail.Should().Be(user.Email);
        bookingDto.OwnerName.Should().Be(user.Name + user.LastName);
        bookingDto.BookingId.Should().Be(booking.BookingId);
        bookingDto.Invited.Should().NotBeNull();
        bookingDto.Invited.Should().BeEmpty();
    }
}