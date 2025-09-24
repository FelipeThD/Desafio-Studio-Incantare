using BackendTraining.Dtos;
using FluentValidation;

namespace BackendTraining.Validators
{
    public class ContactsValidator : AbstractValidator<CreateContactDto>
    {
        public ContactsValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve conter no mínimo 3 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mensagem é obrigatória.")
                .MinimumLength(10).WithMessage("Mensagem deve ter no mínimo 10 caracteres.");
        }
    }
}
