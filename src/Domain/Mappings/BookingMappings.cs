using Domain.DTO.Bookings;
using Domain.Models.BookingModels;

namespace Domain.Mappings;

public static class BookingMappings
{
    public static Booking ToBooking(this BookingCreateRequest bookingCreateRequest, BookingUserStatus user)
    {
        var map = new Booking
        {
            StartDate = bookingCreateRequest.StartDate.ToUniversalTime(),
            EndDate = bookingCreateRequest.EndDate.ToUniversalTime(),
            OwnerMail = bookingCreateRequest.OwnerMail,
            Cancelled = false
        };
        map.BookingUserStatus.Add(user);
        return map;
    }

    public static BookingGetRequest ToBookingGetRequest(this Booking booking)
    {
        List<Invited> invites = new();
        foreach (var item in booking.BookingUserStatus)
        {
            var invitedMap = new Invited
            {
                Email = item.User.Email,
                Accepted = item.Accepted
            };
            if (item.BookingCancelleds.Count != 0)
            {
                invitedMap.Cancelled = true;
            }
            else
            {
                invitedMap.Cancelled = false;
            }
            
            invites.Add(invitedMap);
        }

        var map = new BookingGetRequest
        {
            StartDate = booking.StartDate.ToUniversalTime(),
            EndDate = booking.EndDate.ToUniversalTime(),
            OwnerMail = booking.OwnerMail,
            BookingId = booking.BookingId,
            Invited = invites
        };
        return map;
    }
}