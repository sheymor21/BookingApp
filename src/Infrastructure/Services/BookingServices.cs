using Domain.DTO.Bookings;
using Domain.Interfaces;
using Domain.Mappings;
using Domain.Models.BookingModels;
using Domain.Models.UserModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookingServices : IBookingServices
{
    private readonly DatabaseContext _databaseContext;

    public BookingServices(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddBookingAsync(BookingCreateRequest bookingCreateRequest)
    {
        List<BookingUserStatus> bookingUserStatusList = new();
        var user = await _databaseContext.Users.FirstAsync(w => w.Email == bookingCreateRequest.OwnerMail);
        foreach (var item in bookingCreateRequest.Invitations)
        {
            var invitedUser = await _databaseContext.Users.FirstAsync(w => w.Email == item);
            var bookingUserStatus = new BookingUserStatus
            {
                Accepted = false,
                User = invitedUser
            };
            bookingUserStatusList.Add(bookingUserStatus);
        }

        var bookingUserStatusEnumerable = bookingUserStatusList.AsEnumerable();
        var dbBooking = bookingCreateRequest.ToBooking(bookingUserStatusEnumerable, user);
        await _databaseContext.Bookings.AddAsync(dbBooking);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<List<BookingGetRequest>> GetBookingAsync(string email)
    {
        List<BookingGetRequest> bookingGetRequests = new();
        var userOwner = await _databaseContext.Users.FirstAsync(w => w.Email == email);
        var ownerBookings = await _databaseContext.Bookings
            .Where(w => w.UserId == userOwner.UserId)
            .Select(booking => new
            {
                booking.BookingId,
                StartDate = booking.StartDate.ToUniversalTime(),
                EndDate = booking.EndDate.ToUniversalTime(),
                OwnerMail = booking.User.Email,
                OwnerName = booking.User.Name + booking.User.LastName,
            })
            .ToListAsync();

        foreach (var item in ownerBookings)
        {
            var bookingGetRequest = new BookingGetRequest
            {
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                OwnerMail = item.OwnerMail,
                BookingId = item.BookingId,
                OwnerName = item.OwnerName
            };


            bookingGetRequest.Invited = await GetInvitedAsync(item.BookingId);
            bookingGetRequests.Add(bookingGetRequest);
        }

        await foreach (var item in GetBookingsFromInvitedAsync(userOwner))
        {
            bookingGetRequests.Add(item);
        }

        return bookingGetRequests;
    }

    private async Task<List<Invited>> GetInvitedAsync(Guid bookingId)
    {
        List<Invited> invites = new();
        var bookingUserStatus = await _databaseContext.BookingUserStatus
            .Where(w => w.BookingId == bookingId)
            .Include(x => x.User)
            .ToListAsync();
        foreach (var item in bookingUserStatus)
        {
            var cancelledStatus = await _databaseContext.BookingCancelleds
                .AnyAsync(w => w.BookingUserStatusId == item.BookingUserStatusId);
            var invited = new Invited()
            {
                Name = item.User.Name + item.User.LastName,
                Accepted = item.Accepted,
                Email = item.User.Email
            };
            if (cancelledStatus)
            {
                invited.Cancelled = true;
            }
            else
            {
                invited.Cancelled = false;
            }

            invites.Add(invited);
        }

        return invites;
    }

    private async IAsyncEnumerable<BookingGetRequest> GetBookingsFromInvitedAsync(User userOwner)
    {
        var invitedBookings = await _databaseContext.BookingUserStatus
            .Where(w => w.UserId == userOwner.UserId)
            .Include(x => x.Booking)
            .ThenInclude(x => x.User)
            .Select(booking => new
            {
                bookigns = booking.Booking,
                startDate = booking.Booking.StartDate,
                endDate = booking.Booking.EndDate,
                accepted = booking.Accepted,
                userEmail = booking.User.Email,
                userName = booking.User.Name + booking.User.LastName,
                userBookingEmail = booking.Booking.User.Email,
                userBookingName = booking.Booking.User.Name + booking.Booking.User.LastName,
                bookingStatusId = booking.BookingUserStatusId,
                booking.BookingId
            }).ToListAsync();

        foreach (var item in invitedBookings)
        {
            var booking = new BookingGetRequest
            {
                StartDate = item.startDate,
                EndDate = item.endDate,
                OwnerMail = item.userBookingEmail,
                BookingId = item.BookingId,
                OwnerName = item.userBookingName
            };

            booking.Invited = await GetInvitedAsync(item.BookingId);
            yield return booking;
        }
    }

    public async Task CancelBookingAsync(Guid bookingId, string email)
    {
        var booking = await _databaseContext.Bookings
            .Include(x => x.User)
            .FirstAsync(w => w.BookingId == bookingId);
        if (booking!.User.Email == email)
        {
            booking.Cancelled = true;
            await _databaseContext.SaveChangesAsync();
        }
        else
        {
            var user = await _databaseContext.Users
                .Where(w => w.Email == email)
                .Select(user => new
                {
                    userId = user.UserId,
                    email = user.Email
                }).FirstAsync();

            var bookingUserStatus = await _databaseContext.BookingUserStatus.FirstAsync(x =>
                x.BookingId == bookingId && x.UserId == user.userId);

            var bookingCancelled = new BookingCancelled
            {
                Reason = "Cancelled by user",
                BookingUserStatus = bookingUserStatus
            };
            await _databaseContext.BookingCancelleds.AddAsync(bookingCancelled);
            await _databaseContext.SaveChangesAsync();
        }
    }
}