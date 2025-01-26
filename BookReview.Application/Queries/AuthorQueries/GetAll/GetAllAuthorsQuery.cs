using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.AuthorQueries.GetAll
{
    public class GetAllAuthorsQuery : IRequest<ResultViewModel<List<AuthorViewModel>>>
    {
        public GetAllAuthorsQuery(string? fullName)
        {
            FullName = fullName;
        }

        public string? FullName { get; private set; }
    }
}
