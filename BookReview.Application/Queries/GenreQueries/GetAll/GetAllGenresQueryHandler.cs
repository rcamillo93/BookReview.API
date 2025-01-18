using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.GenreQueries.GetAll
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, ResultViewModel<List<GenreViewModel>>>
    {
        private readonly IGenreRepository _genreRepository;

        public GetAllGenresQueryHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ResultViewModel<List<GenreViewModel>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetAllAsync();

            var viewModel = genres
                                .Select(g => new GenreViewModel(g.Id, g.Description))
                                .ToList();

            return ResultViewModel<List<GenreViewModel>>.Sucess(viewModel);
        }
    }
}
