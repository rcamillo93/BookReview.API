using BookReview.Application.Commads.AuthorCommands.Create;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(a => a.FullName)
                .NotEmpty()
                    .WithMessage("O nome é obrigatório.");
        }
    }
}
