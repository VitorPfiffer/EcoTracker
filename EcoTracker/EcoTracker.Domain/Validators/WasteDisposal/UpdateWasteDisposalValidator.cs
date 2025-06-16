using EcoTracker.Domain.Entities;
using FluentValidation;

namespace EcoTracker.Domain.Validators
{
    public class UpdateWasteDisposalValidator : AbstractValidator<WasteDisposal>
    {
        public UpdateWasteDisposalValidator()
        {
            RuleFor(x => x.WasteType)
                        .NotEmpty().WithMessage("Tipo de resíduo é obrigatório.")
                        .MaximumLength(50).WithMessage("Tipo de resíduo não pode exceder 50 caracteres.")
                        .Matches(@"^[a-zA-Z\u00C0-\u00FF\s]+$").WithMessage("Tipo de resíduo deve conter apenas letras.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.")
                .LessThanOrEqualTo(1000).WithMessage("Quantidade máxima permitida é 1000.");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Unidade de medida é obrigatória.")
                .Must(unit => new[] { "kg", "g", "L", "ml", "un" }.Contains(unit.ToLower()))
                .WithMessage("Unidade inválida. Use: kg, g, L, ml ou un.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Data é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Data não pode ser futura.")
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-1)).WithMessage("Data não pode ser anterior a 1 ano.");


            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("ID do usuário é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do usuário inválido.");
        }
    }
}
