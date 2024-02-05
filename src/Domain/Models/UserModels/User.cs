using Domain.Models.BookingModels;

namespace Domain.Models.UserModels;

public class User
{
    public Guid UserId { get; set; }
    public long Dni { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? NickName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public int Age { get; set; }

    public List<BookingUserStatus>? BookingsStatus { get; set; } = new();
    public List<Booking>? Bookings { get; set; } = new();
}