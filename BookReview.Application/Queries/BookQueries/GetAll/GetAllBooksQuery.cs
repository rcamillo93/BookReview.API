using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.BookQueries.GetAll
{
    public class GetAllBooksQuery : IRequest<ResultViewModel<List<BookViewModel>>>
    {
    }
}
