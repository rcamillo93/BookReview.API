using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.BookQueries.GetAll
{
    public class GetAllBooksQuery : IRequest<ResultViewModel<List<BookViewModel>>>
    {
        public GetAllBooksQuery(int? authorId, string? title)
        {
            AuthorId = authorId;
            Title = title;
        }

        public int? AuthorId { get; private set; }
        public string? Title { get; private set; }
    }
}
