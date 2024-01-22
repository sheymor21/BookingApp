namespace Domain.Models.BookingModels;

public class Booking
{
    public Guid BookingId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? OwnerMail { get; set; }
    public bool Cancelled { get; set; }

    public List<BookingUserStatus> BookingUserStatus { get; set; } = new();

}