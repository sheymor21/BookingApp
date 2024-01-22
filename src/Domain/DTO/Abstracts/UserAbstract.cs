namespace Domain.DTO.Abstracts;

public abstract class UserAbstract
{
    public string? Name { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}