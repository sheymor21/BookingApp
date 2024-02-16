namespace Domain.Interfaces;

public interface IValidatorManager
{
    Task<List<string>> ValidateAsync<T>(T objectToValidate);
}