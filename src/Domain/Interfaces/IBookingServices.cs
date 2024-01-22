using Domain.DTO.Bookings;

namespace Domain.Interfaces;

public interface IBookingServices
{
    Task AddBooking(BookingCreateRequest bookingCreateRequest);
    Task<List<BookingGetRequest>> GetBooking(string email);
}