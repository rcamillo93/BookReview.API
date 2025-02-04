using BookReview.Application.Models;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Create
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ResultViewModel<int>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateReviewCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel<int>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);

            if (book == null)
                return ResultViewModel<int>.Error("Livro não encontrado");

            var review = new Review(request.Description, request.UserId, request.BookId, request.Rating, request.ReadingStartDate);

            book.Reviews.Add(review);
            var qtdReviews = book.Reviews.Count();
             
            book.UpdateAverageGrade(qtdReviews , request.Rating);

            await _bookRepository.SaveChangesAsync();

            return ResultViewModel<int>.Sucess(review.Id);
        }
    }
}
