using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.ReviewQueries.GetById
{
    public class GetReviewByIdQueryCommand : IRequestHandler<GetReviewByIdQuery, ResultViewModel<ReviewDetailsViewModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetReviewByIdQueryCommand(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel<ReviewDetailsViewModel>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _bookRepository.GetReviewByIdAsync(request.Id);

            if (review == null)
                return ResultViewModel<ReviewDetailsViewModel>.Error("Review não encontrado.");

            var viewModel = new ReviewDetailsViewModel(review.Description, review.UserId, review.BookId, review.Rating, review.User.FullName,
                                                review.Book.Title, review.Book.BookCover, review.Book.Author.FullName);

            return ResultViewModel<ReviewDetailsViewModel>.Sucess(viewModel);
        }
    }
}
