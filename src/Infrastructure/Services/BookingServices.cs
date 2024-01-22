using Domain.DTO.Bookings;
using Domain.Interfaces;
using Domain.Mappings;
using Domain.Models.BookingModels;
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

    public async Task AddBooking(BookingCreateRequest bookingCreateRequest)
    {
        var user = await _databaseContext.Users.FirstAsync(w => w.Email == bookingCreateRequest.OwnerMail);
        var bookingUserStatus = new BookingUserStatus
        {
            Accepted = false,
            User = user
        };

        await _databaseContext.Bookings.AddAsync(bookingCreateRequest.ToBooking(bookingUserStatus));
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<List<BookingGetRequest>> GetBooking(string email)
    {
        List<BookingGetRequest> bookingGetRequests = new();
        var user = await _databaseContext.Users
            .Include(x => x.BookingsStatus!)
            .ThenInclude(x => x.Booking)
            .FirstAsync(w => w.Email == email);

        foreach (var item in user.BookingsStatus!)
        {
            bookingGetRequests.Add(item.Booking.ToBookingGetRequest());
        }

        return bookingGetRequests;
    }
}