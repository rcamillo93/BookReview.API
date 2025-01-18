using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.GenreQueries.GetById
{
    public class GetGenreByIdQuery : IRequest<ResultViewModel<GenreViewModel>>
    {
        public GetGenreByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
