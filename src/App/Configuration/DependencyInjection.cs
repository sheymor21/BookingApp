using Domain.DTO.Bookings;
using Domain.DTO.Users;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Services;
using Infrastructure.Validations;

namespace BookingApp.Settings;

public static class DependencyInjection
{
    public static IServiceCollection DepedencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IBookingServices, BookingServices>();
        services.AddScoped<IDummyServices, DummyServices>();
        services.AddScoped<IValidatorManager, ValidatorManger>();
        services.AddScoped<IValidator<UserCreateRequest>, UserCreateValidations>();
        services.AddScoped<IValidator<UserUpdateRequest>, UserUpdateValidations>();
        services.AddScoped<IValidator<BookingCreateRequest>, BookingCreateValidations>();
        services.AddScoped<IValidator<BookingUpdateRequest>, BookingUpdateValidations>();
        return services;
    }
}