using EcoTracker.Core.NotificationManager;
using FluentValidation;

namespace EcoTracker.Core.FluentValidator.ValidatorManager
{
    /// <summary>
    /// Manages validation of objects using a collection of validators and handles validation errors via a notification manager.
    /// </summary>
    internal sealed class ValidatorManager : IValidatorManager
    {
        private readonly INotificationManager _notificationManager;

        private readonly IEnumerable<IValidator> _validators;

        public ValidatorManager(INotificationManager notificationManager, IEnumerable<IValidator> validators)
        {
            _notificationManager = notificationManager;
            _validators = validators;
        }
        /// <summary>
        /// Validates the given object using the specified validator type.
        /// </summary>
        /// <typeparam name="TValidator">The type of the validator to use.</typeparam>
        /// <param name="value">The object to be validated.</param>
        /// <returns>
        /// A boolean indicating whether the validation was successful.
        /// </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> ValidateAsync<TValidator>(object value)
        {
            var type = typeof(TValidator);

            var validator = _validators.FirstOrDefault(x => x.GetType() == type) ?? throw new InvalidOperationException($"Validator of type {type.Name} not found");

            IValidationContext validationContext = new ValidationContext<object>(value);

            var validationResult = await validator.ValidateAsync(validationContext);

            if (!validationResult.IsValid) _notificationManager.NotifyErrors(validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"));
            return validationResult.IsValid;
        }
    }
}
