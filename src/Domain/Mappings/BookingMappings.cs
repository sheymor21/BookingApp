using Domain.DTO.Bookings;
using Domain.Models.BookingModels;
using Domain.Models.UserModels;

namespace Domain.Mappings;

public static class BookingMappings
{
    public static Booking ToBooking(this BookingCreateRequest bookingCreateRequest, IEnumerable<BookingUserStatus> invitedUsers,User user )
    {
        var map = new Booking
        {
            StartDate = bookingCreateRequest.StartDate.ToUniversalTime(),
            EndDate = bookingCreateRequest.EndDate.ToUniversalTime(),
            Cancelled = false,
            User = user
        };
        foreach (var item in invitedUsers)
        {
            map.BookingUserStatus.Add(item);
        }

        return map;
    }
}