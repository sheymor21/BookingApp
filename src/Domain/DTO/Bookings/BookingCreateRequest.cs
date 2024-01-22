using Domain.DTO.Abstracts;

namespace Domain.DTO.Bookings;

public class BookingCreateRequest : BookingAbstract
{
    public List<string> Invitations { get; set; }
}