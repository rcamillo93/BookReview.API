using BookReview.Application.Commads.GenreCommands.Create;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(g => g.Description)
                  .NotEmpty()
                    .WithMessage("A descrição é obrigatória");
        }
    }
}
