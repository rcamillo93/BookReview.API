using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.BookQueries.GetById
{
    public class GetBookByIdQuery : IRequest<ResultViewModel<BookViewModel>>
    {
        public GetBookByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
