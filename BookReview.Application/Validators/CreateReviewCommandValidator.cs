using BookReview.Application.Commads.ReviewCommans.Create;
using FluentValidation;

namespace BookReview.Application.Validators
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(r => r.Description)
                    .NotEmpty()
                        .WithMessage("A descrição é obrigatória.");

            RuleFor(r => r.BookId)
                    .NotEmpty()
                        .WithMessage("Informe o livro");

            RuleFor(r => r.ReadingStartDate)
                    .NotEmpty()
                        .WithMessage("A data de início da leitura é obrigatória.");

            RuleFor(r => r.Rating)
                    .NotEmpty()
                        .WithMessage("A descrição é obrigatória.")
                    .InclusiveBetween(1, 5)
                       .WithMessage("Informe uma nota de 1 à 5.");

            RuleFor(r => r.ReadingStartDate)
                .LessThan(DateTime.Now)
                    .WithMessage("A data de início da leitura deve ser menor que a data da avaliação.");
        }
    }
}
