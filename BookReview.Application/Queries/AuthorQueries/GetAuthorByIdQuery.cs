using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.AuthorQueries
{
    public class GetAuthorByIdQuery : IRequest<ResultViewModel<AuthorViewModel>>
    {
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
