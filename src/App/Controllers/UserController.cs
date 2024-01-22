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

    /// <summary>
    /// Adds a user to the system.
    /// </summary>
    /// <param name="userCreateRequest">The object representing the user creation request.</param>
    /// <returns>An <see cref="ActionResult"/> indicating the outcome of the operation.</returns>
    [HttpPost("")]
    public async Task<ActionResult> AddUser(UserCreateRequest userCreateRequest)
    {
        await _userServices.AddUser(userCreateRequest);
        return Ok();
    }

    /// <summary>
    /// Retrieves a user by their DNI (Documento Nacional de Identidad).
    /// </summary>
    /// <param name="userDni">The DNI of the user to retrieve.</param>
    /// <returns>
    /// <see cref="ActionResult"/> representing the HTTP response with the user information
    /// if found, or NotFound status if the user does not exist.
    /// </returns>
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

    /// <summary>
    /// Updates the user with the specified DNI (Documento Nacional de Identidad) number.
    /// </summary>
    /// <param name="userDni">The DNI number of the user to be updated.</param>
    /// <param name="userUpdateRequest">The user update request containing the updated information.</param>
    /// <returns>
    /// ActionResult representing the updated user if the user exists and the update operation is successful.
    /// HttpStatusCode.OK (200) is returned.
    /// Otherwise, HttpStatusCode.BadRequest (400) is returned.
    /// </returns>
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