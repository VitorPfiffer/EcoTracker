using EcoTracker.Domain.Entities;
using FluentValidation;

namespace EcoTracker.Domain.Validators
{
    public class UpdatePickUpScheduleValidator : AbstractValidator<PickUpSchedule>
    {

        public UpdatePickUpScheduleValidator()
        {
            RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Rua é obrigatória.")
            .MaximumLength(100).WithMessage("Rua não pode ter mais que 100 caracteres.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Número é obrigatório.")
                .MaximumLength(10).WithMessage("Número não pode ter mais que 10 caracteres.");

            RuleFor(x => x.Neighborhood)
                .NotEmpty().WithMessage("Bairro é obrigatório.")
                .MaximumLength(50).WithMessage("Bairro não pode ter mais que 50 caracteres.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Cidade é obrigatória.")
                .MaximumLength(50).WithMessage("Cidade não pode ter mais que 50 caracteres.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("Estado é obrigatório.")
                .Length(2).WithMessage("Estado deve ter 2 caracteres (UF).");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("CEP deve estar no formato 12345-678.");

            RuleFor(x => x.WasteType)
                .NotEmpty().WithMessage("Tipo de resíduo é obrigatório.")
                .MaximumLength(30).WithMessage("Tipo de resíduo não pode ter mais que 30 caracteres.");

            RuleFor(x => x.ScheduledDate)
                .NotEmpty().WithMessage("Data agendada é obrigatória.")
                .GreaterThan(DateTime.Now.Date).WithMessage("Data deve ser futura.");
        }
    }
}
