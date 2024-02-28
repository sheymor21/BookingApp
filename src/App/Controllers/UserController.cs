using Domain.DTO.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers;

public class UserController : BaseController
{
    private readonly IUserServices _userServices;
    private readonly IValidatorManager _validatorManager;

    public UserController(IUserServices userServices, IValidatorManager validatorManager)
    {
        _userServices = userServices;
        _validatorManager = validatorManager;
    }

    /// <summary>
    /// Adds a user to the system.
    /// </summary>
    /// <param name="userCreateRequest">The object representing the user creation request.</param>
    /// <returns>An <see cref="ActionResult"/> indicating the outcome of the operation.</returns>
    [HttpPost("")]
    public async Task<ActionResult> AddUser(UserCreateRequest userCreateRequest)
    {
        var validationErrors = await _validatorManager.ValidateAsync(userCreateRequest);
        if (validationErrors.Count > 0)
        {
            return BadRequest(validationErrors);
        }

        await _userServices.AddUserAsync(userCreateRequest);
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
        var userDniExistence = await _validatorManager.ValidateUserDniAsync(userDni);
        if (!userDniExistence)
        {
            NotFound("The Dni doesn't exist");
        }

        var user = await _userServices.GetUserByDniAsync(userDni);
        if (user is not null)
        {
            return Ok(user);
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
        var validationErrors = await _validatorManager.ValidateAsync(userUpdateRequest);
        if (validationErrors.Count > 0)
        {
            return BadRequest(validationErrors);
        }

        var updateUser = await _userServices.UpdateUserAsync(userDni, userUpdateRequest);
        if (updateUser is not null)
        {
            return Ok(updateUser);
        }

        return BadRequest();
    }
}