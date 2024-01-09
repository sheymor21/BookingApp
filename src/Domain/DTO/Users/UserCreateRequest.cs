namespace Domain.DTO.Users;

public class UserCreateRequest
{
    public string Dni { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
}