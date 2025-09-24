using BackendTraining.Dtos;
using FluentValidation;

namespace BackendTraining.Validators
{
    public class TeamValidator : AbstractValidator<CreateTeamDto>
    {
        public TeamValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve conter no mínimo 3 caracteres.");

            RuleFor(x => x.Bio)
                .MaximumLength(500).WithMessage("A Bio não pode ter mais que 500 caracteres.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Função é obrigatória.")
                .MaximumLength(30).WithMessage("A Função não pode ter mais que 30 caracteres.");
        }
    }
}
