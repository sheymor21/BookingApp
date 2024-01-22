namespace Domain.DTO.Abstracts;

public abstract class BookingAbstract
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? OwnerMail { get; set; }
}