using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.GenreCommands.Create
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, ResultViewModel<int>>
    {
        private readonly IGenreRepository _genreRepository;

        public CreateGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ResultViewModel<int>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre(request.Description);

            await _genreRepository.AddAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return ResultViewModel<int>.Sucess(genre.Id);
        }
    }
}
