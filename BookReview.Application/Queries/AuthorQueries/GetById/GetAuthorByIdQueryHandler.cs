using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.AuthorQueries.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, ResultViewModel<AuthorViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ResultViewModel<AuthorViewModel>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);

            if (author == null)
                return ResultViewModel<AuthorViewModel>.Error("Author não encontrado");

            var viewModel = new AuthorViewModel(author.FullName, author.DateBirth);

            return ResultViewModel<AuthorViewModel>.Sucess(viewModel);
        }
    }
}
