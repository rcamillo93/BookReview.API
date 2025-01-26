using BookReview.Application.Commads.UserCommands.Create;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("O e-mail é inválido");

            RuleFor(u => u.FullName)
                .NotEmpty()
                    .WithMessage("O nome é obrigatório");

            RuleFor(u => u.Password)
                .NotEmpty()
                    .MinimumLength(6);
        }
    }
}
