using BookReview.Application.Models;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.BookCommands.Create
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ResultViewModel<int>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.Title, request.Description, request.ISBN,
                                                      request.AuthorId, request.Publisher, request.GenreId,
                                                      request.PublicationYear, request.QuantityPages, request.BookCover);

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return ResultViewModel<int>.Sucess(book.Id);
        }
    }
}
