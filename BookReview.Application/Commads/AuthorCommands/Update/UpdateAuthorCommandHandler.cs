using BookReview.Application.Models;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.AuthorCommands.Update
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, ResultViewModel>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ResultViewModel> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);

            if(author == null)
                return ResultViewModel.Error("Autor não encontrado");

            author.Update(request.FullName, request.DateBirth);

            await _authorRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
