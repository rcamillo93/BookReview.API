using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.GenreQueries.GetAll
{
    public class GetAllGenresQuery : IRequest<ResultViewModel<List<GenreViewModel>>>
    {
    }
}
