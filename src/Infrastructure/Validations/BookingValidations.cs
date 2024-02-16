using Domain.DTO.Bookings;
using FluentValidation;

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