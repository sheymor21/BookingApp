﻿namespace Domain.Models.UserModels;

public class User
{
    public Guid UserId { get; set; }
    public long Dni { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public int Age { get; set; }
}