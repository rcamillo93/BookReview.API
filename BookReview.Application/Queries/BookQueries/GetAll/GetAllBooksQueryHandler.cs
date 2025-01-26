using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.BookQueries.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ResultViewModel<List<BookViewModel>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel<List<BookViewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(request.AuthorId, request.Title);

            var viewModel = books
                            .Select(b => new BookViewModel(b.Id, b.Title, b.Description, b.ISBN,
                                    b.AuthorId, b.Publisher, b.GenreId, b.PublicationYear, b.QuantityPages,
                                    b.AverageGrade, b.BookCover))
                                    .ToList();

            return ResultViewModel<List<BookViewModel>>.Sucess(viewModel);
        }
    }
}
