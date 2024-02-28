using FluentValidation;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Validations;

public static class CustomValidations
{
    public static IRuleBuilderOptions<T, TElement> CheckBookingExist<T,TElement>(this IRuleBuilderOptions<T, TElement> ruleBuilderOptions,
        DatabaseAppContext databaseAppContext)
    {
        return ruleBuilderOptions.MustAsync(async (value, context) =>
        {
            var bookingExistence =
                await databaseAppContext.Bookings.AnyAsync(w => w.BookingId.ToString() == value!.ToString()!,
                    cancellationToken: context);
            return bookingExistence;
        });
    }
}