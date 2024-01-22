namespace Domain.DTO.Bookings;

public class BookingUpdateRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string>? NewInviteds { get; set; }
    public List<string>? DeleteInviteds { get; set; }
}