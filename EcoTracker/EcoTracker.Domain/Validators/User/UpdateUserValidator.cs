using EcoTracker.Domain.Entities;
using FluentValidation;

namespace EcoTracker.Domain.Validators
{
    public class UpdateUserValidator : AbstractValidator<User>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório.")
                .MinimumLength(3).WithMessage("O nome de usuário deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

            RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Tipo de perfil inválido.");
        }
    }
}
