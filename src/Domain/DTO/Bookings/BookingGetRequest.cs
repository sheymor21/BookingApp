using Domain.DTO.Abstracts;

namespace Domain.DTO.Bookings;

public class BookingGetRequest : BookingAbstract
{
    public Guid BookingId { get; set; }
    public List<Invited> Invited { get; set; }
}

public class Invited
{
    public string? Email { get; set; }
    public bool Accepted { get; set; }
    public bool Cancelled { get; set; }
}