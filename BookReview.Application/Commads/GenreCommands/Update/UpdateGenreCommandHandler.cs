using BookReview.Application.Models;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.GenreCommands.Update
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, ResultViewModel>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ResultViewModel> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Id);

            if (genre is null)
                return ResultViewModel.Error("Gênero não encontrado");

            genre.Update(request.Description);

            await _genreRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
