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
            OwnerMail = booking.User.Email,
            OwnerName = booking.User.Name + booking.User.LastName,
            BookingId = booking.BookingId,
            Invited = invites
        };
        return map;
    }
}