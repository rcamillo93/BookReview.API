using BookReview.Application.Commads.UserCommands.Create;
using BookReview.Application.Commads.UserCommands.ForgotPassword;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class ForgotPassowrdHandlerValidator : AbstractValidator<ForgotPassowrdCommand>
    {
        public ForgotPassowrdHandlerValidator()
        {
            RuleFor(f => f.Email)
                .EmailAddress()
                .WithMessage("O e-mail é inválido");
        }
    }
}
