using Domain.DTO.Abstracts;

namespace Domain.DTO.Users;

public class UserCreateRequest : UserAbstract
{
     public long Dni { get; set; }
}