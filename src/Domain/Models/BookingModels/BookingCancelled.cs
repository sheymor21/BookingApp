﻿
namespace Domain.Models.BookingModels;

public class BookingCancelled
{
    public Guid BookingCancelledId { get; set; }
    public string? Reason { get; set; } = string.Empty;

    public Guid BookingUserStatusId { get; set; }
    public BookingUserStatus BookingUserStatus { get; set; } = new();
}