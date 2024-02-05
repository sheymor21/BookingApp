using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers;

public class DummyController : BaseController
{
    private readonly IDummyServices _dummyServices;

    public DummyController(IDummyServices dummyServices)
    {
        _dummyServices = dummyServices;
    }

    /// <summary>
    /// Adds dummy data to the system.
    /// </summary>
    /// <param name="quantity">The quantity of dummy data to add.</param>
    /// <returns>An ActionResult indicating the status of the operation.</returns>
    [HttpGet("")]
    public async Task<ActionResult> AddDummyData(int quantity)
    {
        await _dummyServices.DummyConstructor(quantity);
        return Ok();
    }

    /// <summary>
    /// Retrieves all users from the system.
    /// </summary>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> of <see cref="UserGetRequest"/> representing the users in the system.</returns>
    [HttpGet("Users")]
    public ActionResult GetAllUsers()
    {
        var result = _dummyServices.GetAllUser();
        return Ok(result);
    }
}