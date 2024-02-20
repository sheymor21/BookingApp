namespace Domain.Interfaces;

public interface IValidatorManager
{
    Task<List<string>> ValidateAsync<T>(T objectToValidate);
    Task<bool> ValidateUserDniAsync(Guid bookingId);
    Task<bool> ValidateUserEmailAsync(string email);
    Task<bool> ValidateUserDniAsync(long userDni);
}