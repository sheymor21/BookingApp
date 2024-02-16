using Domain.Interfaces;
using Domain.Mappings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Validations;

public class ValidatorManger : IValidatorManager
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorManger(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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
}