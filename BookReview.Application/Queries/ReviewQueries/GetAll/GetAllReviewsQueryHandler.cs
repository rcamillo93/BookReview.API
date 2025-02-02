using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.ReviewQueries.GetAll
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, ResultViewModel<List<ReviewViewModel>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllReviewsQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel<List<ReviewViewModel>>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _bookRepository.GetAllReviewsAsync(request.BookTitle);

            var viewModel = reviews
                        .Select(r => new ReviewViewModel(r.Description, r.Book.Title, r.Book.BookCover, r.BookId, r.Rating))
                        .ToList();

            return ResultViewModel<List<ReviewViewModel>>.Sucess(viewModel);
        }
    }
}
