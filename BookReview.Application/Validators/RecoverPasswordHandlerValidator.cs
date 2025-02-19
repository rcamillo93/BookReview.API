using BookReview.Application.Commads.UserCommands.Create;
using BookReview.Application.Commads.UserCommands.RecoveryPassword;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class RecoverPasswordHandlerValidator : AbstractValidator<RecoverPasswordCommand>
    {
        public RecoverPasswordHandlerValidator()
        {
            RuleFor(r => r.Email)
                .EmailAddress()
                .WithMessage("O e-mail é inválido");


            RuleFor(r => r.TemporaryPassword)
                .NotEmpty()
                    .WithMessage("A senha temporária é obrigatória.");

            RuleFor(r => r.NewPassword)
              .NotEmpty()
                  .MinimumLength(6);
        }
    }
}
