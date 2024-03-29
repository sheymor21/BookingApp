﻿using Domain.DTO.Bookings;
using Domain.Models.BookingModels;
using Microsoft.EntityFrameworkCore;
using test.Data;

namespace test;

[Collection("Database")]
public class BookingShould
{
    private readonly BookingServices _bookingServices;
    private readonly Fixture _fixture;
    private readonly CreatorManager _creator;
    private readonly DatabaseAppContext _appContextFixture;

    public BookingShould()
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
            .Select(p => new
            {
                booking = new Booking
                {
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Cancelled = p.Cancelled,
                },
                ownerEmail = p.User.Email,
                invitedEmail = p.BookingUserStatus.Select(x=>x.User.Email).First()
            })
            .FirstOrDefaultAsync();

        bookingDb?.booking.Should().NotBeNull().And.BeEquivalentTo(bookingCreateRequest, config =>
            config.Excluding(x => x.Invitations)
                .Excluding(x => x.OwnerMail)
        );

        bookingDb?.ownerEmail.Should().Be(bookingCreateRequest.OwnerMail);
        bookingDb?.invitedEmail.Should().Be(bookingCreateRequest.Invitations[0]);
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

        var bookingDb = await _appContextFixture.Bookings.FirstOrDefaultAsync();
        bookingDb.Should().NotBeNull().And.BeEquivalentTo(bookingUpdateRequest, config =>
            config.Excluding(x => x.NewInviteds)
                .Excluding(x => x.DeleteInviteds)
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

    [Fact]
    public async Task BookingCancelByOwnerTest()
    {
        var user = await _creator.UserFixtureGeneratorAsync();
        var booking = await _creator.BookingFixtureGeneratorAsync(user);

        BookingCancelRequest bookingCancelRequest = new()
        {
            BookingId = booking.BookingId,
            Email = user.Email!,
            Reason = _fixture.Create<string>()
        };
        await _bookingServices.CancelBookingAsync(bookingCancelRequest);
        var bookingCancelledDb = await _appContextFixture.Bookings
            .Select(p => p.Cancelled)
            .FirstOrDefaultAsync();

        bookingCancelledDb.Should().Be(true);
    }

    [Fact]
    public async Task BookingCancelByInvitedTest()
    {
        var users = await _creator.UserFixtureGeneratorAsync(2);
        var booking = await _creator.BookingFixtureGeneratorAsync(users[0]);
        await _creator.BookingUserStatusFixtureGenerator(booking, users[1]);

        BookingCancelRequest bookingCancelRequest = new()
        {
            BookingId = booking.BookingId,
            Email = users[1].Email!,
            Reason = _fixture.Create<string>()
        };
        await _bookingServices.CancelBookingAsync(bookingCancelRequest);
        var bookingCancelledDb = await _appContextFixture.BookingCancelleds
            .Include(x => x.BookingUserStatus)
            .ThenInclude(x => x.User)
            .Select(p => new
            {
                p.Reason,
                invitedEmail = p.BookingUserStatus.User.Email
            })
            .FirstOrDefaultAsync();

        bookingCancelledDb?.Reason.Should().NotBeNull().And.Be(bookingCancelRequest.Reason);
        bookingCancelledDb?.invitedEmail.Should().NotBeNull().And.Be(bookingCancelRequest.Email);
    }
}