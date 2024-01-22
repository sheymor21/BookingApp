using Domain.Models.UserModels;

namespace Domain.Models.BookingModels;

public class BookingUserStatus
{
    public Guid BookingUserStatusId { get; set; }
    public bool Accepted { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; }
    
    public List<BookingCancelled> BookingCancelleds = new();
}