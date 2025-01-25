using BookReview.Application.Commads.BookCommands.Create;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(b => b.ISBN)
                .NotEmpty()
                .WithMessage("O ISBN é obrigatório");

            RuleFor(b => b.PublicationYear)
                .NotNull();

            RuleFor(b => b.Title)
                .NotEmpty()
                .NotNull()
                    .WithMessage("O nome titulo é obrigatório")
                .MaximumLength(150)
                    .WithMessage("O tamanho máximo do campo Titulo é 150 caracteres");

            RuleFor(b => b.Description)
               .NotEmpty()
               .NotNull()
                   .WithMessage("A nome descrição é obrigatória")
               .MaximumLength(250)
                   .WithMessage("O tamanho máximo do campo Descrição é 250 caracteres");

            RuleFor(b => b.AuthorId)
              .NotEmpty()
              .WithMessage("O Autor é obrigatório");

            RuleFor(b => b.GenreId)
              .NotEmpty()
              .WithMessage("O Gênero é obrigatório");

            RuleFor(b => b.QuantityPages)
              .InclusiveBetween(20, 2000)
                .WithMessage("Informe a quantidade de páginas.")
              .NotEmpty()
                .WithMessage("A quantidade de páginas é obrigatória");
        }
    }
}
