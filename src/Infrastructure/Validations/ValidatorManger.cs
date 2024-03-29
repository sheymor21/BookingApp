﻿using Domain.Interfaces;
using Domain.Mappings;
using FluentValidation;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Validations;

public class ValidatorManger : IValidatorManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DatabaseAppContext _databaseAppContext;

    public ValidatorManger(IServiceProvider serviceProvider, DatabaseAppContext databaseAppContext)
    {
        _serviceProvider = serviceProvider;
        _databaseAppContext = databaseAppContext;
    }

    public async Task<List<string>> ValidateAsync<T>(T objectToValidate)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();

        if (validator is null)
        {
            throw new ValidationException($"A validator for {typeof(T).FullName} doesn't exist");
        }

        var validationResult = await validator.ValidateAsync(objectToValidate);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors.ToStringList();
        }

        return [];
    }

    public async Task<bool> ValidateUserDniAsync(Guid bookingId)
    {
        var existence = await _databaseAppContext.Bookings.AnyAsync(w => w.BookingId == bookingId);
        return existence;
    }
    public async Task<bool> ValidateUserDniAsync(long userDni)
    {
        var existence = await _databaseAppContext.Users.AnyAsync(w => w.Dni == userDni);
        return existence;
    }

    public async Task<bool> ValidateUserEmailAsync(string email)
    {
        var existence = await _databaseAppContext.Users.AnyAsync(w => w.Email == email);
        return existence;
    }
}