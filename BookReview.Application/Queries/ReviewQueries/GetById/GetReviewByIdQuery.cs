using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.ReviewQueries.GetById
{
    public class GetReviewByIdQuery : IRequest<ResultViewModel<ReviewDetailsViewModel>>
    {
        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
