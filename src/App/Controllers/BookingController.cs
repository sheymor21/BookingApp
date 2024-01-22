using Domain.DTO.Bookings;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers;

public class BookingController : BaseController
{
    private readonly IBookingServices _bookingServices;

    public BookingController(IBookingServices bookingServices)
    {
        _bookingServices = bookingServices;
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
        await _bookingServices.AddBooking(bookingCreateRequest);
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
        var result = await _bookingServices.GetBooking(email);
        return Ok(result);
    }
}