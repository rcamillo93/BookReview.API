using BookReview.Application.Models;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.AuthorCommands.Create
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ResultViewModel<int>> {

        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ResultViewModel<int>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author(request.FullName, request.DateBirth);

            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();

            return ResultViewModel<int>.Sucess(author.Id);

        }
    }
}
