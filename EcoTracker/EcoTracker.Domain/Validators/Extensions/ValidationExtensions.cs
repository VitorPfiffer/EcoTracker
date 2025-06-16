using EcoTracker.Domain.Interfaces.Repositories;
using FluentValidation;

namespace EcoTracker.Domain.Validators.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> UniqueEmail<T>(
            this IRuleBuilder<T, string> ruleBuilder, IUserRepository userRepository)
        {
            return ruleBuilder.MustAsync(async (email, cancellation) =>
            {
                var user = await userRepository.GetByEmailAsync(email);
                return user == null;
            }).WithMessage("Este e-mail já está em uso.");
        }

        public static IRuleBuilderOptions<T, string> UniqueUsername<T>(
    this IRuleBuilder<T, string> ruleBuilder, IUserRepository userRepository)
        {
            return ruleBuilder.MustAsync(async (username, cancellation) =>
            {
                var user = await userRepository.GetByUsernameAsync(username);
                return user == null;
            }).WithMessage("Este usuário já está em uso.");
        }
    }

}
