namespace Domain.DTO.Bookings;

public class BookingCancelRequest
{
    public Guid BookingId { get; set; }
    public string Email { get; set; }
    public string? Reason { get; set; }
}