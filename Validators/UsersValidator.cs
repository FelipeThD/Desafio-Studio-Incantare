using BackendTraining.Dtos;
using FluentValidation;

namespace BackendTraining.Validators
{
    public class UsersValidator : AbstractValidator<LoginDto>
    {
        public UsersValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório")
                .MinimumLength(4).WithMessage("O nome de usuário deve conter no mínimo 4 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória")
                .MinimumLength(3).WithMessage("A senha deve conter no mínimo 3 caracteres");
        }
    }
}
