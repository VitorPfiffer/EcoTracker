namespace EcoTracker.Core.FluentValidator.ValidatorManager
{
    public interface IValidatorManager
    {
        Task<bool> ValidateAsync<TValidator>(object value);
    }
}
