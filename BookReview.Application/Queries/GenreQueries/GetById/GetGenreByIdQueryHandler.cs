using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.GenreQueries.GetById
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, ResultViewModel<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;

        public GetGenreByIdQueryHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ResultViewModel<GenreViewModel>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Id);

            if (genre is null)
                return ResultViewModel<GenreViewModel>.Error("Gênero não encontrado");

            var viewModel = new GenreViewModel(genre.Id, genre.Description);

            return ResultViewModel<GenreViewModel>.Sucess(viewModel);
        }
    }
}
