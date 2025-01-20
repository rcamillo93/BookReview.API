using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Queries.BookQueries.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ResultViewModel<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel<BookViewModel>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book is null)
                return ResultViewModel<BookViewModel>.Error("Livro não encontrado");

            var viewModel = new BookViewModel(book.Id, book.Title, book.Description, book.ISBN, book.AuthorId,
                                            book.Publisher, book.GenreId, book.PublicationYear, book.QuantityPages,
                                            book.AverageGrade, book.BookCover);

            return ResultViewModel<BookViewModel>.Sucess(viewModel);
        }
    }
}
