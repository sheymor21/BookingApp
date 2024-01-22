namespace Domain.DTO.Bookings;

public class BookingAcceptRequest
{
    public Guid BookingId { get; set; }
    public string Email { get; set; }
}