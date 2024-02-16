using Domain.DTO.Users;
using FluentValidation;

namespace Infrastructure.Validations;

public class UserCreateValidations : AbstractValidator<UserCreateRequest>
{
    public UserCreateValidations()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Dni).Must(w => w.ToString().Length <= 9);
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Age).NotEmpty();
    }
}

public class UserUpdateValidations : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateValidations()
    {
        RuleFor(x=>x.Email).EmailAddress();
        RuleFor(x=>x.LastName).NotEmpty();
        RuleFor(x=>x.FirstName).NotEmpty();
        RuleFor(x=>x.Age).NotEmpty();
    }
}