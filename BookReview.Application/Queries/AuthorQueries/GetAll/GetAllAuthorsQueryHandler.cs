using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.AuthorQueries.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, ResultViewModel<List<AuthorViewModel>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ResultViewModel<List<AuthorViewModel>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAllAsync();

            var model = author
                        .Select(a => new AuthorViewModel(a.FullName, a.DateBirth))
                        .ToList();

            return ResultViewModel<List<AuthorViewModel>>.Sucess(model);
        }
    }
}
