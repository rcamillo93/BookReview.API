using BookReview.Application.Models;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Delete
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ResultViewModel>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteReviewCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _bookRepository.GetReviewByIdAsync(request.Id);

            if (review == null)
                return ResultViewModel.Error("Review não encontrado");

            var qtdReview = await _bookRepository.CountReviewsByBookId(review.BookId) -1;

            review.Delete(true, qtdReview);

            await _bookRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
