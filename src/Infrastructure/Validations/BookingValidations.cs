using Domain.DTO.Bookings;
using FluentValidation;
using Infrastructure.Context;

namespace Infrastructure.Validations;

public class BookingCreateValidations : AbstractValidator<BookingCreateRequest>
{
    public BookingCreateValidations()
    {
        RuleFor(x=>x.StartDate).NotEmpty();
        RuleFor(x=>x.EndDate).NotEmpty();
        RuleFor(x=>x.OwnerMail).NotEmpty().EmailAddress();
        RuleForEach(x => x.Invitations).EmailAddress();
    }
}

public class BookingUpdateValidations : AbstractValidator<BookingUpdateRequest>
{
    public BookingUpdateValidations()
    {
        RuleFor(x=>x.StartDate).NotEmpty();
        RuleFor(x=>x.EndDate).NotEmpty();
    }
}

public class BookingCancelValidations : AbstractValidator<BookingCancelRequest>
{
    public BookingCancelValidations(DatabaseAppContext context)
    {
        RuleFor(x=>x.BookingId).NotEmpty().CheckBookingExist(context);
        RuleFor(x=>x.Email).NotEmpty();
    }
}