using Domain.Models.UserModels;

namespace Domain.Models.BookingModels;

public class Booking
{
    public Guid BookingId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Cancelled { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = new();
    public List<BookingUserStatus> BookingUserStatus { get; set; } = new();
}