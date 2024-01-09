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

    [HttpPost("")]
    public async Task<ActionResult> AddUser(UserCreateRequest userCreateRequest)
    {
        await _userServices.AddUser(userCreateRequest);
        return Ok();
    }

    [HttpGet("")]
    public async Task<ActionResult> GetUser(long userDni)
    {
        var result = await _userServices.GetUserByDni(userDni);
        if (result is not null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPut("")]
    public async Task<ActionResult> UpdateUser(long userDni, UserUpdateRequest userUpdateRequest)
    {
        var result = await _userServices.UpdateUser(userDni, userUpdateRequest);
        if (result is not null)
        {
            return Ok(result);
        }
        return BadRequest();
    }
}