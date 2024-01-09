using Domain.DTO.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers;

public class UserController : BaseController
{
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<ActionResult> AddUser(UserCreateRequest userCreateRequest)
    {
        await _userServices.AddUser(userCreateRequest);
        return Ok();
    }
}