using Domain.DTO.Bookings;

namespace Domain.Interfaces;

public interface IBookingServices
{
    Task<List<BookingGetRequest>> GetBookingAsync(string email);
    Task AddBookingAsync(BookingCreateRequest bookingCreateRequest);
    Task CancelBookingAsync(Guid bookingId, string email);
    Task UpdateBookingAsync(Guid bookingId, BookingUpdateRequest bookingUpdateRequest);
}