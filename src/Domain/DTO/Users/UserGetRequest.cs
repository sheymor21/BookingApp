using Domain.DTO.Abstracts;

namespace Domain.DTO.Users;

public class UserGetRequest : UserAbstract
{
    public long Dni { get; set; }
}