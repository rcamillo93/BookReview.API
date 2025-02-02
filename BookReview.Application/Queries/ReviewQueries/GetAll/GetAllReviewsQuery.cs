using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.ReviewQueries.GetAll
{
    public class GetAllReviewsQuery : IRequest<ResultViewModel<List<ReviewViewModel>>>
    {
        public GetAllReviewsQuery(string? bookTitle)
        {
            BookTitle = bookTitle;
        }

        public string? BookTitle { get; set; }
    }
}
