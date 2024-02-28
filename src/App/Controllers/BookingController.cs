using Domain.DTO.Bookings;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers;

public class BookingController : BaseController
{
    private readonly IBookingServices _bookingServices;
    private readonly IValidatorManager _validatorManager;

    public BookingController(IBookingServices bookingServices, IValidatorManager validatorManager)
    {
        _bookingServices = bookingServices;
        _validatorManager = validatorManager;
    }

    /// <summary>
    /// Adds a new booking.
    /// </summary>
    /// <param name="bookingCreateRequest">The booking creation request containing the details of the booking.</param>
    /// <returns>
    /// An ActionResult representing the result of the operation.
    /// </returns>
    [HttpPost("")]
    public async Task<ActionResult> AddBooking(BookingCreateRequest bookingCreateRequest)
    {
        var validationErrors = await _validatorManager.ValidateAsync(bookingCreateRequest);
        if (validationErrors.Count > 0)
        {
            return BadRequest(validationErrors);
        }

        await _bookingServices.AddBookingAsync(bookingCreateRequest);
        return Ok();
    }

    /// <summary>
    /// Retrieve bookings for a given email.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve bookings for.</param>
    /// <returns>An ActionResult object containing the bookings for the specified email address.</returns>
    [HttpGet("")]
    public async Task<ActionResult> GetBookings(string email)
    {
        var emailExistence = await _validatorManager.ValidateUserEmailAsync(email);
        if (!emailExistence)
        {
            return BadRequest("The email doesn't exist");
        }

        var result = await _bookingServices.GetBookingAsync(email);
        return Ok(result);
    }

    /// <summary>
    /// Cancels a booking for the specified email and booking ID.
    /// </summary>
    /// <param name="bookingCancelRequest">The booking cancellation request containing the booking ID, email and optional reason.</param>
    /// <returns>An ActionResult representing the result of the operation.</returns>
    [HttpPut("Cancel")]
    public async Task<ActionResult> CancelBooking(BookingCancelRequest bookingCancelRequest)
    {
        var validationsErrors = await _validatorManager.ValidateAsync(bookingCancelRequest);
        if (validationsErrors.Count > 0)
        {
            return NotFound(validationsErrors);
        }

        await _bookingServices.CancelBookingAsync(bookingCancelRequest);
        return Ok();
    }

    /// <summary>
    /// Updates a booking with the specified booking ID and new booking details.
    /// </summary>
    /// <param name="bookingId">The ID of the booking to update.</param>
    /// <param name="bookingUpdateRequest">The updated booking details.</param>
    /// <returns>An ActionResult representing the result of the operation.</returns>
    [HttpPut("")]
    public async Task<ActionResult> UpdateBookings(Guid bookingId, BookingUpdateRequest bookingUpdateRequest)
    {
        var bookingExistence = await _validatorManager.ValidateUserDniAsync(bookingId);
        if (!bookingExistence)
        {
            return NotFound("Booking ID doesn't exist");
        }

        var validationErrors = await _validatorManager.ValidateAsync(bookingUpdateRequest);
        if (validationErrors.Count > 0)
        {
            return BadRequest(validationErrors);
        }

        await _bookingServices.UpdateBookingAsync(bookingId, bookingUpdateRequest);
        return Ok();
    }
}