using FluentValidation.Results;

namespace Domain.Mappings;

public static class ValidatorsMappings
{
    public static List<string> ToStringList(this List<ValidationFailure> failures)
    {
        List<string> errors = new();
        foreach (var item in failures)
        {
            errors.Add($"{item.PropertyName}: {item.ErrorMessage}");
        }
        return errors;
    }
}